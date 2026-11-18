(function () {
    "use strict";
    angular.module("webapp").controller("homeController", homeController);

    homeController.$inject = ['$scope', 'requestService', 'serverConfig'];
    function homeController($scope, requestService, serverConfig) {
        
        //requestService.requestClientToken().then(function (response) {
        //    serverConfig.authToken1 = response.data.tokenKey;
        //});
    }

})();