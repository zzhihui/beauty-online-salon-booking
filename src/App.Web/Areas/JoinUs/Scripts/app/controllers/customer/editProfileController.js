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