(function () {
    "use strict";

    angular.module("webapp").controller("myBookingController", myBookingController);

    myBookingController.$inject = ['$scope', 'requestService', 'authenticationService'];
    function myBookingController($scope, requestService, authenticationService) {
        
        //Get booking history by customer id and shop id From DB
        requestService.getCustomerBookingHistory().then(function (response) {
            if (response.data == null) {
                $scope.resultMessage = "You don't have any bookings";
            } else {
                $scope.bookingItems = response.data;
            }
            
            console.log(response.data);
            $scope.errorOccurs = false;
        }).catch(function (err) {
            $scope.errorOccurs = true;
            $scope.error = "Error loading service data.";
        });

        $scope.updated = function() {
            //Get booking history by customer id and shop id From DB
            requestService.getCustomerBookingHistory().then(function (response) {
                if (response.data == null) {
                    $scope.resultMessage = "You don't have any bookings";
                } else {
                    $scope.bookingItems = response.data;
                }

                console.log(response.data);
                $scope.errorOccurs = false;
            }).catch(function (err) {
                $scope.errorOccurs = true;
                $scope.error = "Error loading service data.";
            });
        };

        $scope.cancel = function(bookingId) {
            if (window.confirm("Are you sure you want to cancel")) {
                requestService.cancelBooking(bookingId).then(function (response) {

                    requestService.getCustomerBookingHistory().then(function (response) {
                        $scope.bookingItems = response.data;

                        console.log(response.data);
                        $scope.errorOccurs = false;
                    }).catch(function (err) {
                        $scope.errorOccurs = true;
                        $scope.error = "Error loading service data.";
                    });

                }).catch(function (err) {
                    //$scope.error = "Failed to Login. Please try again?";
                });
            }
            
        }

        //$scope.login = function () {
        //    requestService.loginCustomer($scope.email, $scope.password).then(function (response) {
                
        //        authenticationService.saveCredential(response.data);
        //        //window.history.back();
        //        window.location.href = 'Demo#!/';

        //    }).catch(function (err) {
        //        $scope.error = "Failed to Login. Please try again?";
        //    });
        //}
    }
})();