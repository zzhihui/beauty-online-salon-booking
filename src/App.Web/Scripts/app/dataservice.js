
angular.module('webapp.core', [])
        .factory('dataservice', dataservice);

var configHeader = {
    'BeautyFairyAuthToken': '',
    'Content-Type': 'application/json'
};

var websiteUrl = 'https://beautyfairy.ie';
//var websiteUrl = 'http://localhost:49041';


/* @ngInject */
function dataservice($http, $location, $q) {

    //Service Functions
    var service = {
        getShopInfo: getShopInfo
    };
    return service;


    //Support function
    function showBusy() {
        //TODO: Busy indicator
    }
    function hideBusy() {

        //TODO: Busy indicator
    }

    function rest(request) {

        var deferred = $q.defer();
        showBusy();
        return $http(request).
           success(function (data) {
               hideBusy();
               deferred.resolve(data);
               return deferred.promise;
           }).
           error(function (data) {
               hideBusy();
               console.log("failure message: " + JSON.stringify({ data: data }));
               deferred.reject();
               $location.url('/');
           });
    }


    function getShopInfo() {
        return rest({ method: 'GET', url: websiteUrl.concat("/api/ShopApi/GetShopInfo"), headers: configHeader });
    }



}
