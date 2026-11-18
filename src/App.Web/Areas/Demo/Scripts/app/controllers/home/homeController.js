(function () {
    "use strict";
    angular.module("webapp").controller("homeController", homeController);

    homeController.$inject = ['$scope', 'requestService', 'serverConfig'];
    function homeController($scope, requestService, serverConfig) {

        //Get Service From DB
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