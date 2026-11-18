(function () {
    "use strict";

    angular.module("webapp").controller("bookingController", bookingController);

    bookingController.$inject = ['$scope', 'requestService', 'authenticationService'];
    function bookingController($scope, requestService, authenticationService) {

        $scope.showCalendar = true;

        $('#datetimepicker_dark').datetimepicker({ theme: 'dark' });
        
        $('.glyphicon-calendar').click(function () {
            if ($scope.showCalendar) {
                $('#datetimepicker_dark').datetimepicker('show');
                $scope.showCalendar = false;
            }
            else {
                $('#datetimepicker_dark').datetimepicker('hide');
                $scope.showCalendar = true;
            }
        });


        //Get Service From DB


        //$scope.register = function () {
        //    requestService.registerCustomer($scope.customer).then(function (response) {
                
        //        authenticationService.saveCredential(response.data);
        //        window.history.back();

        //    }).catch(function (err) {
        //        console.log(err);
        //        $scope.error = err["data"];
        //    });
        //}
    }
})();