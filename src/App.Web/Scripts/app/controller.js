(function () {
    'use strict';

    angular.module('webapp.controller', []);
    angular.module('webapp.controller').controller('homeController', homeController);
  

    //Home Controller
    homeController.$inject = ['$scope', '$rootScope'];
    function homeController($scope, $rootScope) {

        $scope.myInterval = 3000;
        $scope.slides = [
          {
              image: 'http://lorempixel.com/400/300/'
          },
          {
              image: 'http://lorempixel.com/400/300/food'
          },
          {
              image: 'http://lorempixel.com/400/300/sports'
          },
          {
              image: 'http://lorempixel.com/400/300/people'
          }
        ];
    }

})();