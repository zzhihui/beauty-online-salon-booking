(function () {
    'use strict';
    angular.module("customerAppointmentManagementApp", ["ngResource"])
    .factory('Appointment', ['$resource', 'BASE_URL', function ($resource, BASE_URL) {
        return $resource(BASE_URL + 'appointment/:id', { id: '@id' },
                    {
                        'update': { method: 'PUT' }
                    });
    }
    ])
    .controller("customerAppointmentManagementCtrl", function ($scope, Appointment, $resource, BASE_URL, $uibModal, $log) {
        // 这里没有添加限制用户等逻辑, 但正常来讲也不用添加这些, 这里的显示逻辑也有问题, 待整合时进行修改
        $scope.appointments = Appointment.query();

        $scope.changeThis = function (index) {
            var appointment = $scope.appointments[index];
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'appointment.modal.html',
                controller: 'appointmentModalCtrl',
                size: 'sm',
                resolve: {
                    bookingItem: function () {
                        // 获取并传入美发服务
                        var Service = $resource(BASE_URL + 'hair-services/:id', { id: '@id' });
                        return Service.get({ id: appointment.serviceId });
                    },
                    bookedTime: function () {
                        // 传入上次预约时间
                        return new Date(appointment.applicationTime * 1000);
                    },
                    bookButtonText: function () {
                        return 'Change';
                    }
                }
            });

            modalInstance.result.then(function (bookingTime) {
                // 修改预约时间的逻辑
                var bookingTimestamp = bookingTime / 1000;
                var temp = appointment.applicationTime;
                appointment.applicationTime = bookingTimestamp;
                Appointment.update({ id: appointment.id }, appointment, function (data) {
                    // 更新成功的逻辑, 这里没弄明白啥情况会出现错误
                    $log.log(data);
                    alert("change success!");
                }, function () {
                    // 更新失败时回退
                    alert("can't change!");
                    appointment.applicationTime = temp;
                });
            }, function () {
                //用户取消选择后的逻辑
                $log.log("canceled");
            });
        };

        $scope.cancelThis = function (index) {
            var appointment = $scope.appointments[index];
            appointment.$delete(function () {
                $scope.appointments.splice(appointment, 1);
            });
        };
    });
})();