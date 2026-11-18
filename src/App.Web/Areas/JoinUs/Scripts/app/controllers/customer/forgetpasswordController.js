(function () {
    "use strict";

    angular.module("webapp").controller("forgetpasswordController", forgetpasswordController);

    forgetpasswordController.$inject = ['$scope', 'requestService'];

    function forgetpasswordController($scope, requestService)
    {
        $scope.PasswordRecovery = function () {
            requestService.passwordRecovery($scope.email).then(function (response) {

                $scope.result = "Your password has been sent to your email address.";
                $scope.errorOccurs = false;

            }).catch(function (err) {
                $scope.error = "Failed to send email. Please check your email address?";
                $scope.errorOccurs = true;
            });
        }
    }
})();