(function () {
    "use strict";

    angular.module("webapp").controller("contactFormController", contactFormController);

    contactFormController.$inject = ['$scope', 'requestService'];

    function contactFormController($scope, requestService)
    {
        $scope.send = function () {
            requestService.sendContactForm($scope.formData).then(function (response) {
                
                $scope.resultMessage = "Your message has been submitted successfully.";
                $scope.result = true;

            }).catch(function (err) {
                $scope.error = "Failed to send message. Please try again?";
                $scope.result = false;
            });
        }
    }
})();