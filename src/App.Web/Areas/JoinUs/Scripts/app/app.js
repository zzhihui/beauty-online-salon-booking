(function () {
    'use strict'; 
    
    var app = angular.module('webapp', ['ngRoute', 'ngCookies', 'ui.bootstrap', 'angular.filter']);

    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        $routeProvider.when("/", {
            controller: "homeController",
            templateUrl: "/JoinUs/Home/Home"
        });
        $routeProvider.when("/gallery", {
            controller: "galleryController",
            templateUrl: "/JoinUs/Home/Gallery"
        });

        $routeProvider.when("/contact", {
            controller: "contactusController",
            templateUrl: "/JoinUs/Home/ContactUs"
        });

        $routeProvider.when("/me", {
            controller: "meController",
            templateUrl: "/JoinUs/Customer/MyProfile"
        });

        $routeProvider.when("/profile", {
            controller: "editProfileController",
            templateUrl: "/JoinUs/Customer/EditProfile"
        });

        $routeProvider.when("/bookings", {
            controller: "myBookingController",
            templateUrl: "/JoinUs/Customer/MyBookings"
        });

        $routeProvider.when("/about", {
            controller: "aboutController",
            templateUrl: "/JoinUs/Home/About"
        });

        $routeProvider.when("/category", {
            controller: "bookingController",
            templateUrl: "/JoinUs/Home/Booking"
        });

        $routeProvider.when("/login", {
            controller: "loginController",
            templateUrl: "/JoinUs/Customer/Login"
        });

        $routeProvider.when("/forgetpassword", {
            controller: "forgetpasswordController",
            templateUrl: "/JoinUs/Customer/ForgetPass"
        });

        $routeProvider.when("/register", {
            controller: "registerController",
            templateUrl: "/JoinUs/Customer/Register"
        });

        $routeProvider.when("/hair", {
            controller: "hairController",
            templateUrl: "/JoinUs/Home/Hair"
        });

        $routeProvider.when("/nail", {
            controller: "nailController",
            templateUrl: "/JoinUs/Home/Nail"
        });

        $routeProvider.when("/logout", {
            controller: "logoutController",
            templateUrl: "/JoinUs/Customer/logoff"
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