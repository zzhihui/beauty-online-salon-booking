(function () {
    "use strict";
    
    angular.module("webapp").controller("hairController", hairController);

    hairController.$inject = ['$scope', 'requestService', 'authenticationService'];
    function hairController($scope, requestService, authenticationService) {

        $scope.showCalendar = true;
        $scope.bookable = true;

        var dateToDisable = new Date();
        dateToDisable.setDate(dateToDisable.getDate() + 2);

        var currentCalendarDate = new Date();
        
        //var logic = function () {
        //    this.setOptions({
        //        minTime: '9:00',
        //        maxTime: '19:00'
        //    });
        //};

        var dateCompare = function(date1, date2) {

            if (date1.getYear() === date2.getYear() && date1.getMonth() === date2.getMonth() && date1.getDate() === date2.getDate())
                return true;
            else
                return false;
        };

        var logic = function (currentDateTime) {

            //console.log(currentDateTime.getDate());
            //console.log(currentCalendarDate.getDate());
            //console.log(currentCalendarDate.getHours() + 2 + ':00');
            
            if (currentDateTime && currentDateTime.getDay() === 0) {
                this.setOptions({
                    minTime: '11:00',
                    maxTime: '18:00'
                });
            }
            else if (currentDateTime && dateCompare(currentDateTime, currentCalendarDate)) {
                this.setOptions({
                    minTime: currentCalendarDate.getHours() + 2 + ':00',
                    maxTime: '18:00'
                });
            } else
                this.setOptions({
                    minTime: '8:00',
                    maxTime: '20:00'
                });
        };

        $('#datetimepicker_dark').datetimepicker({
            theme: 'dark',
            minDate: 0,
            step: 15,
            onChangeDateTime: logic,
            onShow: logic,
            isALlowTimesInit: true,
            onGenerate: function (ct) {
                //$(this).find('.xdsoft_date.xdsoft_weekend')
                //    .addClass('xdsoft_disabled');
            },
            beforeShowDay: function (date) {
                if (date.getMonth() === dateToDisable.getMonth() && date.getDate() === dateToDisable.getDate()) {
                    return [false, ""];
                }

                return [true, ""];
            }
        });

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

        $scope.cutFilter = function (service) {
            return service.category === 'Gents' || service.category === 'Ladies';
        };

        $scope.errorOccurs = false;

        //Get Service From DB
        requestService.getServiceDetails().then(function (response) {
            $scope.bookingItems = response.data;
            console.log(response.data);
            $scope.errorOccurs = false;
        }).catch(function (err) {
            $scope.errorOccurs = true;
            $scope.error = "Error loading service data.";
        });

        //Check datetime
        $scope.validateDateTime = function() {
            console.log($scope.bookingTime);

            if ($scope.bookingTime === '' || $scope.bookingTime === undefined
                || $scope.bookingTime === null || isNaN(Date.parse($scope.bookingTime))) {
                $scope.errorOccurs = true;
                $scope.error = "Please select date time.";
                return false;
            }

            return true;
        }

        //Booking Request
        $scope.bookingRequest = function(serviceId, selectedDatetime) {
            requestService.bookService(serviceId, selectedDatetime).then(function (response) {

                $scope.resultMessage = "Your booking has been sent, a confirmation email has been sent to your email.";
                console.log(response.data);
                $scope.errorOccurs = false;
            }).catch(function (err) {
                console.log(err);
                $scope.errorOccurs = true;
                $scope.error = err.data; //"Booking failed, please try again?";
            });
        }

        //Quick Booking
        $scope.quickBook = function() {
            if (!$scope.validateDateTime())
                return;

            console.log($scope.serviceSelect);
            
            if ($scope.serviceSelect === undefined) {
                $scope.errorOccurs = true;
                $scope.error = "Please select a service.";
                return;
            }

            $scope.bookingRequest($scope.serviceSelect, $scope.bookingTime);
        }

        //Booking
        $scope.book = function (serviceId) {
            
            if (!$scope.validateDateTime())
                return;

            console.log(serviceId);

            $scope.bookingRequest(serviceId, $scope.bookingTime);
        }
    }
})();