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
            controller: "bookingController",
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