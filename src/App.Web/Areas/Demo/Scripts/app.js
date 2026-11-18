(function () {
    'use strict'; 
    
    var app = angular.module('webapp', ['ngRoute', 'ngCookies', 'ui.bootstrap', 'angular.filter']);

    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        $routeProvider.when("/", {
            controller: "homeController",
            templateUrl: "/Demo/Home/Home"
        });
        $routeProvider.when("/gallery", {
            controller: "galleryController",
            templateUrl: "/Demo/Home/Gallery"
        });

        $routeProvider.when("/contact", {
            controller: "contactusController",
            templateUrl: "/Demo/Home/ContactUs"
        });

        $routeProvider.when("/me", {
            controller: "meController",
            templateUrl: "/Demo/Customer/MyProfile"
        });

        $routeProvider.when("/profile", {
            controller: "editProfileController",
            templateUrl: "/Demo/Customer/EditProfile"
        });

        $routeProvider.when("/bookings", {
            controller: "myBookingController",
            templateUrl: "/Demo/Customer/MyBookings"
        });

        $routeProvider.when("/about", {
            controller: "aboutController",
            templateUrl: "/Demo/Home/About"
        });

        $routeProvider.when("/category", {
            //controller: "serviceListController",
            templateUrl: "/Demo/Home/Booking"
        });

        $routeProvider.when("/login", {
            controller: "loginController",
            templateUrl: "/Demo/Customer/Login"
        });

        $routeProvider.when("/forgetpassword", {
            controller: "forgetpasswordController",
            templateUrl: "/Demo/Customer/ForgetPass"
        });

        $routeProvider.when("/register", {
            controller: "registerController",
            templateUrl: "/Demo/Customer/Register"
        });

        $routeProvider.when("/hair", {
            //controller: "hairController",
            templateUrl: "/Demo/Home/Hair"
        });

        $routeProvider.when("/nail", {
            //controller: "nailController",
            templateUrl: "/Demo/Home/Nail"
        });

        $routeProvider.when("/logout", {
            controller: "logoutController",
            templateUrl: "/Demo/Customer/logoff"
        });

        $routeProvider.otherwise({
            redirectTo: "/"
        });
        // Specify HTML5 mode (using the History APIs) or HashBang syntax.
        $locationProvider.html5Mode(false).hashPrefix('!');
    }]);

    app.run(['$rootScope', '$cookieStore', 'serverConfig', 'utilService', 'authenticationService', function ($rootScope, $cookieStore, serverConfig, utilService, authenticationService) {

        authenticationService.loadCredential();
       
        $rootScope.$on('$locationChangeStart', function () {
            $rootScope.previousPage = location.pathname;
            $rootScope.$broadcast('user-change');
        });
    }]);
})();
(function () {
    'use strict';

    //TODO: localisation
    angular.module('webapp').constant('serverConfig', 
    {
        url: 'https://beautyfairy.ie',
        //url: 'http://localhost:49041',
        clientKey: 'REPLACE_ME',
        authToken1: '',
        authToken2: '',
        shopId:2
    });
})();
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
(function () {
    "use strict";

    angular.module("webapp").controller("editProfileController", editProfileController);

    editProfileController.$inject = ['$scope', 'requestService'];

    function editProfileController($scope, requestService)
    {
        $scope.master = {
            "name": "Zhihui Zhao",
            "credit": '10',
            "email": "amtzhao.zhihui@gmail.com"
        };

        $scope.update = function (user) {
            $scope.master = angular.copy(user);
        };

        $scope.reset = function (form) {
            if (form) {
                form.$setPristine();
                form.$setUntouched();
            }
            $scope.user = angular.copy($scope.master);
        };

        $scope.reset();
    }
})();
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
(function () {
    "use strict";

    function loginController($scope, requestService, authenticationService) {
        $scope.login = function () {
            requestService.loginCustomer($scope.email, $scope.password).then(function (response) {
                
                authenticationService.saveCredential(response.data);
                window.history.back();
                //window.location.href = 'Demo#!/';

            }).catch(function (err) {
                $scope.error = "Failed to Login. Please try again?";
            });
        }
    }

    angular.module("webapp").controller("loginController", loginController);

    loginController.$inject = ['$scope', 'requestService', 'authenticationService'];
})();
(function () {
    "use strict";

    angular.module("webapp").controller("logoutController", logoutController);

    logoutController.$inject = ['$scope', 'authenticationService', '$location'];
    function logoutController($scope, authenticationService, $location) {
        authenticationService.clearCredential();
        $location.url('/');

    }

})();
(function () {
    "use strict";

    angular.module("webapp").controller("meController", meController);

    meController.$inject = ['$scope', '$rootScope', 'requestService', 'authenticationService'];

    function meController($scope, $rootScope, requestService, authenticationService) {
        
        //Get customer detail from controller api.
        if ($rootScope.globals.currentUser != null) {

            var user = $rootScope.globals.currentUser;

            //var res = str.split(" ");

            $scope.master = {
                credit: '99',
                firstName: user.firstName,
                lastName: user.lastName,
                email: user.email,
                address: user.address,
                phoneNumber: user.phoneNumber
            };

            //requestService.getCustomerDetails($rootScope.globals.currentUser.id).then(function (response) {

            //    var user = response.data;
            //    console.log($rootScope.globals.currentUser);
            //    alert(user.contactName);
            //    $scope.master = {
            //        credit: '99',
            //        firstName: user.contactName,
            //        lastName: user.contactName,
            //        email: user.email,
            //        address: user.address,
            //        phoneNumber: user.phoneNumber
            //    };
            //}).catch(function (err) {
            //    $scope.error = "Failed to Login. Please try again?";
            //});
        }

        $scope.Update = function (customer) {
            $scope.master = angular.copy(customer);

            //Save to database
            //if (!$scope.customer.changePassword) {

            customer.id = $rootScope.globals.currentUser.id;
            customer.email = $rootScope.globals.currentUser.email;

            requestService.updateCustomerDetails(customer).then(function (response) {
                console.log(response.data);

                authenticationService.saveCredential(response.data);
                $rootScope.$broadcast('user-change');

                $scope.resultMessage = "Your details have been saved.";
                $scope.error = "";
                //var user = response.data;
                //console.log($rootScope.globals.currentUser);
                //alert(user.contactName);
                //$scope.master = {
                //    credit: '99',
                //    firstName: user.contactName,
                //    lastName: user.contactName,
                //    email: user.email,
                //    address: user.address,
                //    phoneNumber: user.phoneNumber
                //};
            }).catch(function (err) {
                console.log(err);
                $scope.error = "Error update profile. Please try again?";
                $scope.resultMessage = "";
            });
            //}
        };

        $scope.Reset = function (form) {
            if (form) {
                form.$setPristine();
                form.$setUntouched();
            }
            $scope.customer = angular.copy($scope.master);
            $scope.resultMessage = "";
        };

        $scope.Reset();
    }
})();
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
(function () {
    "use strict";

    angular.module("webapp").controller("registerController", registerController);

    registerController.$inject = ['$scope', 'requestService', 'authenticationService'];
    function registerController($scope, requestService, authenticationService) {
        $scope.register = function () {
            requestService.registerCustomer($scope.customer).then(function (response) {
                
                authenticationService.saveCredential(response.data);
                window.history.back();

            }).catch(function (err) {
                console.log(err);
                $scope.error = err["data"];
            });
        }
    }
})();
(function () {
    "use strict";

    angular.module("webapp").controller("errorController", errorController);

    errorController.$inject = ["$scope"];
    function errorController($scope) {

    }

})();
(function () {
    "use strict";

    angular.module("webapp").controller("aboutController", aboutController);

    aboutController.$inject = ["$scope"];
    function aboutController($scope) {

    }

})();
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
(function () {
    "use strict";

    angular.module("webapp").controller("contactusController", contactusController);

    contactusController.$inject = ["$scope"];
    function contactusController($scope) {}
})();
(function () {
    "use strict";

    angular.module("webapp").controller("galleryController", galleryController);

    galleryController.$inject = ["$scope"];
    function galleryController($scope) {

    }

})();
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

            $('#datetimepicker_dark').datetimepicker('show');
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

                $scope.enableBooking = false;
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
(function () {
    "use strict";
    angular.module("webapp").controller("homeController", homeController);

    homeController.$inject = ['$scope', 'requestService', 'serverConfig'];
    function homeController($scope, requestService, serverConfig) {

        requestService.getUniqueServiceCategories().then(function (response) {
            $scope.Categories = response.data;
            console.log(response.data);
            $scope.errorOccurs = false;
        }).catch(function (err) {
            $scope.errorOccurs = true;
            $scope.error = "Error loading service data.";
            });

        //requestService.requestClientToken().then(function (response) {
        //    serverConfig.authToken1 = response.data.tokenKey;
        //});
    }

})();
(function () {
    "use strict";

    angular.module("webapp").controller("nailController", nailController);

    nailController.$inject = ['$scope', 'requestService', 'authenticationService'];
    function nailController($scope, requestService, authenticationService) {

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

        var dateCompare = function (date1, date2) {

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
        $scope.validateDateTime = function () {
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
        $scope.bookingRequest = function (serviceId, selectedDatetime) {
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
        $scope.quickBook = function () {
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
(function () {
    "use strict";

    angular.module("webapp").controller("navController", navController);

    navController.$inject = ['$scope', '$rootScope', 'utilService', 'authenticationService'];
    function navController($scope, $rootScope, utilService, authenticationService) {
        
        $scope.$on('order-updated', function () {
            if (utilService.isUndefinedOrNull($rootScope.myOrder)) {
                $scope.countNumber = "";
            }
            else {
                if ($rootScope.myOrder.orderItems.length == 0) {
                    $scope.countNumber = "";
                } else {
                    $scope.countNumber = $rootScope.myOrder.orderItems.length.toString();
                }
            }
        });
        $scope.$on('loading', function () {
            //Do nothing...
        });

        $scope.$on('user-change', function () {

            if (authenticationService.isAuthenticated()) {
                $scope.userloggedin = true;
                $scope.fullName = $rootScope.globals.currentUser.firstName + ' ' + $rootScope.globals.currentUser.lastName;
            } else {
                $scope.userloggedin = false;
                $scope.fullName = "";
            }
        });
    }
})();
(function () {
    "use strict";
    
    angular.module("webapp").controller("serviceListController", serviceListController);

    serviceListController.$inject = ['$scope', '$rootScope', '$filter', 'requestService', 'authenticationService'];
    function serviceListController($scope, $rootScope, $filter, requestService, authenticationService) {

        $scope.userloggedin = false;

        if ($rootScope.globals != null && $rootScope.globals.currentUser != null) {

            $scope.user = $rootScope.globals.currentUser;
            $scope.userloggedin = true;
        }

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
            //theme: 'dark',
            //minDate: 0,
            step: 30, 
            //onChangeDateTime: logic,
            //onShow: logic,
            closeOnTimeSelect: true,
            ////scrollTime: true,
            //isALlowTimesInit: true,
            onGenerate: function (ct) {
                $(this).find('.xdsoft_date.xdsoft_weekend')
                    .addClass('xdsoft_disabled');
            },
            beforeShowDay: function (date) {
                if (date.getMonth() === dateToDisable.getMonth() && date.getDate() === dateToDisable.getDate()) {
                    return [false, ""];
                }

                return [true, ""];
            }
        });


        $('.date').change(
            function () {
                //alert("here"); continue here.
                requestService.getAvailableSlots($scope.selectedServiceId, $('.date').val(), $scope.selectedUserId).then(function (response) {
                    //alert($scope.selectedServiceId);
                    //alert($scope.selectedUserId);
                    $scope.errorOccurs = false;
                    $scope.availableSlots = response.data;
                    console.log(response.data);
                    if (response.data === undefined || response.data.length === 0) {
                        $scope.resultMessage = "No slot is available, please pick another date.";
                        console.log(response.data);
                        $scope.bookSuccess = true;

                        $scope.dateSelected = false;
                    } else {
                        $scope.dateSelected = true;
                    }
                }).catch(function (err) {
                    $scope.errorOccurs = true;
                    $scope.error = "Error loading service data.";
                });

            });
        

        $('.glyphicon-calendar').click(function () {
            if ($('.datepicker-days').css('display') == 'block') {
                $('.date').datepicker('hide');
            } else {
                $('.date').datepicker('show');
            }

            //if ($scope.showCalendar) {
            //    $('#datetimepicker_dark').datetimepicker('show');
            //    $scope.showCalendar = false;
            //}
            //else {
            //    $('#datetimepicker_dark').datetimepicker('hide');
            //    $scope.showCalendar = true;
            //}
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
            $scope.serviceSelect = "" + $scope.bookingItems[0].id;
            $scope.serviceUser = "-1";
            //alert($scope.serviceSelect);
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
        $scope.bookingRequest = function (serviceId, userId, selectedDatetime) {
            requestService.bookService(serviceId, userId, selectedDatetime).then(function (response) {
                //alert(userId);
                $scope.resultMessage = "Your booking has been sent, a confirmation email has been sent to your email.";
                console.log(response.data);
                $scope.bookSuccess = true;
                $scope.errorOccurs = false;
                $scope.enableBooking = false;
            }).catch(function (err) {
                console.log(err);
                $scope.bookSuccess = false;
                $scope.errorOccurs = true;
                $scope.error = err.data; //"Booking failed, please try again?";
            });
        }

        $scope.showDateTime = function (serviceId, serviceUserId) {
            var service0 = $filter('filter')($scope.bookingItems, { 'id': serviceId });
            //var stylist0 = $filter('filter')($scope.bookingItems, { 'id': serviceId });
            console.log(service0[0].name);
            //console.log(service0[0]);

            var stylist0 = "Any Stylist";
            if (serviceUserId != -1) {
                var stylist1 = $filter('filter')(service0[0].serviceUsers, { 'id': serviceUserId });
                stylist0 = stylist1[0].displayName;
            }
                

            console.log(stylist0);

            $scope.bookingTitle = service0[0].name + " with " + stylist0;
            
            $scope.bookSuccess = false;
            $scope.errorOccurs = false;
            $scope.dateSelected = false;
            $scope.enableBooking = true;
            $('.date').val('');
            $(".btn-time").removeClass("btn-time-active");

            $('#dateTimeModal').modal('show');

            $scope.selectedServiceId = serviceId;
            $scope.selectedUserId = serviceUserId;

            //if (!$scope.validateDateTime())
            //    return;

            //console.log($scope.serviceSelect);

            //if ($scope.serviceSelect === undefined) {
            //    $scope.errorOccurs = true;
            //    $scope.error = "Please select a service.";
            //    return;
            //}

            //$scope.bookingRequest($scope.serviceSelect, -1, $scope.bookingTime);
        }

        $scope.setTime = function (event, selectedTime) {
            //alert(selectedTime);
            $scope.enableBooking = true;
            $scope.selectedTime = selectedTime;
            $(".btn-time").removeClass("btn-time-active");

            $(event.target).addClass('btn-time-active');

            //$(".btn-time").removeClass("btn-time-active");

            //$(this).addClass("btn-time-active");
        };

        //Quick Booking
        $scope.quickBook = function () {
            //alert($(".btn-time:active").prop("id"));

            //$('#dateTimeModal').modal('show');

            //alert($scope.bookingTime);
            //alert($scope.selectedTime);

            if (!$scope.validateDateTime())
                return;

            console.log($scope.serviceSelect);

            //alert($scope.selectedServiceId);
            //alert($scope.selectedUserId);

            if ($scope.serviceSelect === undefined) {
                $scope.errorOccurs = true;
                $scope.error = "Please select a service.";
                return;
            }

            $scope.bookingRequest($scope.selectedServiceId, $scope.selectedUserId, "" + $scope.bookingTime + " "+ $scope.selectedTime);
        }

        //Booking
        $scope.book = function (serviceId, shopUserId) {
            
            if (!$scope.validateDateTime())
                return;

            console.log(serviceId);

            //alert($scope.selectedServiceId);
            //alert($scope.serviceUser);
            
            $scope.bookingRequest($scope.selectedServiceId, shopUserId, $scope.bookingTime);
        }
    }
})();
(function () {
    'use strict';

    angular.module('webapp').directive('backImg', function () {
        return function (scope, element, attrs) {
            var url = attrs.backImg;
            element.css({
                'background-image': 'url(' + url + ')',
                'background-size': '300px 150px',
                'background-position': 'center',
                'background-repeat': 'no-repeat'
            });
        };
    });
})();
(function () {
    'use strict';

    angular.module('webapp').factory('convertEuro', convertEuro);
    function convertEuro() {
        return function (input) {
            return '€' + input;
        };
    }

})();
(function () {
    'use strict';

    angular.module('webapp').factory('shortText', shortText);
    function shortText() {
        return function (input) {
            if (angular.isUndefined(val) || val === null)
                return '';
            else
                return '';
                
        };
    }

})();
(function () {
    'use strict';

    angular.module('webapp').factory('authenticationService', authenticationService);
    authenticationService.$inject = ['$rootScope', '$cookieStore', 'serverConfig', 'utilService'];
    function authenticationService($rootScope, $cookieStore, serverConfig, utilService) {

        //Service Functions
        var service = {
            saveCredential: saveCredential,
            clearCredential: clearCredential,
            loadCredential: loadCredential,
            isAuthenticated: isAuthenticated
        };
        return service;

        function isAuthenticated() {
            return !utilService.isUndefinedOrNull($rootScope.globals) && !utilService.isUndefinedOrNull($rootScope.globals.currentUser);
        }

        function clearCredential() {

            $rootScope.globals = {};
            $cookieStore.remove('globals');
        }

        function cacheCredential(globals) {
            $cookieStore.put('globals', globals);
        }

        function saveCredential(user) {
            clearCredential();
            user.password = "";
            $rootScope.globals.currentUser = user;
            serverConfig.authToken2 = user.clientToken;
            cacheCredential($rootScope.globals);
        }

        function loadCredential() {
            var credential = $cookieStore.get('globals');
            if (!utilService.isUndefinedOrNull(credential)) {
                $rootScope.globals = credential;
                serverConfig.authToken2 = $rootScope.globals.currentUser.clientToken;
            }
        }
    }
})();
(function () {
    'use strict';

    angular.module('webapp').factory('dataService', dataService);
    dataService.$inject = ['$http', '$location', '$q', '$rootScope'];
    function dataService($http, $location, $q, $rootScope) {

        //Service Functions
        var service = {

            getNewOrder: getNewOrder,
            calculateTotal: calculateTotal
        };
        return service;

        //Support function
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }
        function newGuid() {

            return s4() + s4() + "-" + s4() + "-" + s4() + "-" +
              s4() + "-" + s4() + s4() + s4();
        }


        //Public functions 
        function getNewOrder() {
            var order = {
                id: 0,
                guid: newGuid()
            };
            return angular.copy(order);
        }

        function calculateTotal(order) {
            var totalValue = 0;
            if (angular.isUndefined(order) || order == null)
                return totalValue;

            angular.forEach(order.orderItems, function (item) {
                totalValue += item.price;
            });

            order.orderOriginalTotal = totalValue;
            order.orderTotal = totalValue;

            return totalValue;
        }



    }
})();
(function () {
    'use strict';

    angular.module('webapp').factory('requestService', requestService);

    requestService.$inject = ['$http', '$location', '$q', '$rootScope', 'serverConfig'];
    function requestService($http, $location, $q, $rootScope, serverConfig) {

        //Service Functions
        var service = {
            requestClientToken: requestClientToken,
            loginCustomer: loginCustomer,
            getShopInfo: getShopInfo,
            registerCustomer: registerCustomer,
            passwordRecovery: passwordRecovery,
            sendContactForm: sendContactForm,
            getCustomerDetails: getCustomerDetails,
            updateCustomerDetails: updateCustomerDetails,
            getServiceDetails: getServiceDetails,
            bookService: bookService,
            getCustomerBookingHistory: getCustomerBookingHistory,
            cancelBooking: cancelBooking,
            getUniqueServiceCategories: getUniqueServiceCategories,
            getAvailableSlots : getAvailableSlots
        };
        return service;

        //Support function
        function generateServiceUrl(baseUrl, params) {
            var paramString = "";

            for (var key in params) {
                if (params.hasOwnProperty(key)) {
                    paramString += key + "=" + params[key] + "&";
                }
            }
            return serverConfig.url.concat(baseUrl) + "?" + paramString.substring(0, paramString.length - 1);
        }

        function generateconfigHeader() {

           var configHeader =
           {
               'BeautyFairyAuthToken': serverConfig.authToken1,
               'BeautyFairyAdminToken': serverConfig.authToken2,
               'Content-Type': 'application/json'
           };

           return configHeader;
        }

        function showBusy() {
            $rootScope.busy = true;
            $rootScope.$broadcast('loading');
        }
        function hideBusy() {

            $rootScope.busy = false;
            $rootScope.$broadcast('loading');
        }
        function rest(request, customError) {

            var deferred = $q.defer();
            showBusy();
            return $http(request).
               success(function (data) {
                   hideBusy();
                   deferred.resolve(data);
                   return deferred.promise;
               }).
               error(function (data, status) {
                   hideBusy();
                   console.log("failure message: " + JSON.stringify({ data: data }));
                   deferred.reject();
                   if (customError === undefined) {
                       if (status == 401) {
                           $location.url('/login');

                       } else {
                           $location.url('/');
                       }
                   }
               });
        }

        //Public functions 
        function requestClientToken() {
            var url = "/api/AuthenticationApi/RequestClientToken";
            var params = {
                clientKey: serverConfig.clientKey,
                shopid: serverConfig.shopId
            };
            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() });
        }

        function bookService(serviceId, userId, slot) {
         
            var url = "/api/ShopApi/BookService";

            var params = {
                ShopId: serverConfig.shopId,
                ServiceId: serviceId,
                ShopUserId: userId,
                DateTimeSlot: slot,
                CustomerId: $rootScope.globals.currentUser.id
            };
            
            var param = {
                shopId: serverConfig.shopId
            };
            
            return rest({ method: 'POST', url: generateServiceUrl(url, param), data: params, headers: generateconfigHeader() }, true);
        }

        function getAvailableSlots(serviceSelect, bookingTime, serviceUser)
        {
            var url = "/api/ShopApi/getAvailableSlots";

            var params = {
                shopId: serverConfig.shopId,
                startDateTime: bookingTime,
                serviceId: serviceSelect,
                shopUserId: serviceUser
            };

            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() }, true);
        }

        function getServiceDetails() {
            var url = "/api/ShopApi/GetAllServices";

            var params = {
                shopId: serverConfig.shopId
            };
            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() }, true);
        }

        function getUniqueServiceCategories() {
            var url = "/api/ShopApi/GetUniqueServiceCategories";

            var params = {
                shopId: serverConfig.shopId
            };
            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() }, true);
        }

        function cancelBooking(bookingId) {
            var url = "/api/CustomerApi/CancelBooking";

            var params = {
                shopId: serverConfig.shopId,
                customerId: $rootScope.globals.currentUser.id,
                bookingId: bookingId
            };
            return rest({ method: 'PUT', url: generateServiceUrl(url, params), headers: generateconfigHeader() }, true);
        }

        function getCustomerBookingHistory() {
            var url = "/api/CustomerApi/GetBookingHistory";

            var params = {
                shopId: serverConfig.shopId,
                customerId: $rootScope.globals.currentUser.id
            };
            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() }, true);
        }

        function loginCustomer(email, password) {

            var url = "/api/CustomerApi/LoginCustomer";

            var params = {
                email: email,
                password: password,
                shopId: serverConfig.shopId
            };
            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() }, true);
        }

        function updateCustomerDetails(user) {
            var url = "/api/CustomerApi/UpdateCustomer";

            var param = {
                shopId: serverConfig.shopId
            };

            var params = {
                Id: user.id,
                ContactName: user.firstName + ' ' + user.lastName,
                FirstName: user.firstName,
                LastName: user.lastName,
                Email: user.email,
                Address: user.address,
                MobileNumber: user.phoneNumber,
                PhoneNumber: user.phoneNumber,
                IsActive: true,
                Password: user.newPassword,
                ConfirmPassword: user.confirmNewPassword,
                OldPassword: user.currentPassword
            };

            return rest({ method: 'PUT', url: generateServiceUrl(url, param), data: params, headers: generateconfigHeader() }, true);
        }

        function getCustomerDetails(customerId) {
            var url = "/api/CustomerApi/GetCustomerById";

            var params = {
                id: customerId,
                shopId: serverConfig.shopId
            };
            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() }, true);
        }

        function passwordRecovery(email) {
            var url = "/api/CustomerApi/PasswordRecovery";

            var params = {
                email: email,
                shopId: serverConfig.shopId
            };
            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() }, true);
        }

        function sendContactForm(data) {
            var url = "/api/CustomerApi/SaveContactForm";

            var param = {
                shopId: serverConfig.shopId
            };
            
            var params = {
                Message: data.inputMessage,
                Subject: data.inputSubject,
                Email: data.inputEmail,
                Name: data.inputName,
                Mobile: data.inputMobile
            };

            return rest({ method: 'POST', url: generateServiceUrl(url, param), data: params, headers: generateconfigHeader() }, true);
        }

        function registerCustomer(user) {
            var url = "/api/CustomerApi/RegisterCustomer";
            
            var param = {
                shopId: serverConfig.shopId
            };
            
            var params = {
                ContactName: user.firstName + ' ' + user.lastName,
                FirstName: user.firstName,
                LastName: user.lastName,
                Email: user.email,
                Address: user.address,
                MobileNumber: user.phoneNumber,
                PhoneNumber: user.phoneNumber,
                Password: user.password,
                ConfirmPassword: user.confirmPassword,
                IsActive: true
            };
            
            return rest({ method: 'POST', url: generateServiceUrl(url, param), data: params, headers: generateconfigHeader() }, true);
        }

        function getShopInfo() {

            var url = "/api/ShopApi/GetShopInfo";
            var params = {
                shopid: serverConfig.shopId
            };
            return rest({ method: 'GET', url: generateServiceUrl(url, params), headers: generateconfigHeader() });
        }
    }
})();
(function () {
    'use strict';

    angular.module('webapp').factory('utilService', utilService);
    utilService.$inject = ['$location'];
    function utilService($location) {

        //Services
        var service = {
            isUndefinedOrNull: isUndefinedOrNull,
            newGuid: newGuid,
            newRef: newRef,
            routing: routing
        };
        return service;

        //Support function 
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }


        function isUndefinedOrNull(val) {
            return angular.isUndefined(val) || val === null;
        }
        function newGuid() {

            return s4() + s4() + "-" + s4() + "-" + s4() + "-" +
              s4() + "-" + s4() + s4() + s4();
        }
        function newRef() {
            return s4() + s4();
        }


        function routing(path) {
            $location.url(path);
        }

    }


})();