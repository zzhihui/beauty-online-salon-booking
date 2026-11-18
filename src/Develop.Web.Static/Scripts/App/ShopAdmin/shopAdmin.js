(function () {
    'use strict';
    angular.module("shopAdminApp", ['ngResource'])
    .factory("Appointment", ['$resource', 'BASE_URL', function ($resource, BASE_URL) {
        return $resource(BASE_URL + 'appointment/:id', { id: '@id' },
                    {
                        'update': { method: 'PUT' }
                    });
    }])
    .controller("bookingManagementCtrl", ['$scope', 'Appointment', '$uibModal', '$log', function ($scope, Appointment, $uibModal, $log) {
        $scope.appointments = Appointment.query();

        $scope.confirm = function (index) {
            var appointment = $scope.appointments[index];
            appointment.approvalResult = 'confirm';
            appointment.approverId = "user2's id";
            appointment.approvalTime = Number.parseInt((new Date()) / 1000);
            appointment.rejectReason = '';
            Appointment.update({ id: appointment.id }, appointment, function (data) {
                $log.log(data);
            });
        };

        $scope.reject = function (index) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'reject.modal.html',
                controller: 'rejectCtrl',
                size: 'sm',
                resolve: {
                    appointment: function () {
                        return $scope.appointments[index];
                    }
                }
            });

            modalInstance.result.then(function (reason) {
                var appointment = $scope.appointments[index];
                appointment.approverId = "user2's id";
                appointment.approvalTime = Number.parseInt((new Date()) / 1000);
                appointment.approvalResult = 'reject';
                appointment.rejectReason = reason;
                
                Appointment.update({ id: appointment.id }, appointment, function (data) {
                    $log.log(data);
                });
            });
        };
    }])
    .controller("rejectCtrl", ["$scope", "$uibModalInstance", "$document", "appointment", function ($scope, $uibModalInstance, $document, appointment) {
        $scope.appointment = appointment;

        $scope.reasons = [
            { value: 'no_hairdresser', description: "Not enough hairdressers!" },
            { value: 'not_bussiness_time', description: "Not in bussiness time!" },
            { value: 'no_material', description: "Not enough serivce materials!" }
        ];

        $scope.reject = function () {
            $uibModalInstance.close($scope.reason);
        }

        // 取消拒绝
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }]);
})();