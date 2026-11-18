(function () {
    'use strict';

    var app = angular.module('webapp', ['webapp.controller', 'webapp.core', 'ngRoute', 'ngCookies', 'ui.bootstrap']);

    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        $routeProvider.when("/", {
            controller: "homeController",
            templateUrl: "/Home/Home"
        });

        $routeProvider.otherwise({
            redirectTo: "/"
        });
        // Specify HTML5 mode (using the History APIs) or HashBang syntax.
        $locationProvider.html5Mode(false).hashPrefix('!');
    }]);

   
})();