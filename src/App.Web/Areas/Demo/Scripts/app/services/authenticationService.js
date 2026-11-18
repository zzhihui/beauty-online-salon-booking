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