(function (angular) {
    var sge = angular.module('sgeApp');

    'use strict';
    sge.factory('AuthInterceptorFactory', ['$q', '$location', 'localStorageService', 'StoreFactory', 
        function ($q, $location, localStorageService, store) {

            var self = this,
                local = {
                    request: function (config) {

                        config.headers = config.headers || {};

                        var authData = store.authentication;

                        if (authData) {
                            config.headers.Authorization = 'Bearer ' + authData.token;
                        }

                        return config;
                    },
                    responseError: function (rejection) {
                        if (rejection.status === 401) {
                            $location.path('/home');
                        }
                        return $q.reject(rejection);
                    }
                };

            self.request = local.request;
            self.responseError = local.responseError;

            return self;
        }
    ]);

}(window.angular));