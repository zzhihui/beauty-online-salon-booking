(function () {
    'use strict';
    angular.module("bookingListApp", ["ngResource", "appointmentModal"])
    .controller("bookingListCtrl", function ($scope, $resource, BASE_URL, $uibModal, $log) {
        var List = $resource(BASE_URL + 'hair-services/:id', { id: '@id' });
        var Appointment = $resource(BASE_URL + 'appointment/:id', { id: '@id' });

        $scope.bookingItems = List.query();

        $scope.bookThis = function (index) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'appointment.modal.html',
                controller: 'appointmentModalCtrl',
                size: 'sm',
                resolve: {
                    bookingItem: function () {
                        return $scope.bookingItems[index];
                    },
                    bookedTime: null,
                    bookButtonText : function () {
                        return 'Book it!';
                    }
                }
            });

            modalInstance.result.then(function (bookingTime) {
                // 用户提交预约后的逻辑
                var bookingTimestamp = bookingTime / 1000;
                var appointment = new Appointment();
                appointment.applicantId = "user1's id";
                appointment.serviceId = $scope.bookingItems[index].id;
                appointment.applicationTime = bookingTimestamp;
                appointment.approvalResult = 'pending';
                appointment.$save(function (appointment) {
                    // 预约后, 此处添加跳转至已预约列表
                    $log.log(appointment);
                });
            }, function () {
                //用户取消选择后的逻辑
                $log.log("canceled");
            });
        };
    })
    
})();