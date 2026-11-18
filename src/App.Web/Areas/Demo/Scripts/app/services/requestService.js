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
            getUniqueServiceCategories: getUniqueServiceCategories
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