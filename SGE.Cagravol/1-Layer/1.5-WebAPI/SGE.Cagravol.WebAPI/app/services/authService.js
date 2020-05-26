

(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.factory('AuthService', ['$window', '$http', '$q', 'localStorageService', 'StoreFactory', 'AppService', 'NetService', 'FlagService',
        function (root, $http, $q, localStorageService, store, app, net, flag) {
            var self = this,
                local = {                    
                    saveRegistration: function (registration, fromReservation) {
                        var successFn, failFn, p;
                        fromReservation = fromReservation || false;
                        local.logOut();

                        successFn = function (response) {
                            return response;
                        };

                        failFn = function (errorResponse) {
                            window.console.log(errorResponse);
                            return errorResponse;
                        };

                        if (fromReservation === true) {
                            p = net.post.registerReservation(registration);
                        } else {
                            p = net.post.registration(registration);
                        }
                        

                        return p.then(successFn, failFn);
                    },
                    login: function (loginData) {
                        var data,
                            p,
                            q = $q.defer(),
                            successFn,
                            failFn;

                        successFn = function (response) {

                            app.setAuthData(loginData.userName, response.access_token, response.roleFlag, response.name, response.rpId);
                            app.persistAuthData();

                            app.setRecentProjectId(parseInt(response.rpId || 0));
                            
                            q.resolve(response);
                        };

                        failFn = function (err, status) {
                            local.logOut();
                            q.reject(err);
                        };

                        p = net.token(loginData.userName, loginData.password);

                        p.then(successFn, failFn);

                        return q.promise;
                    },
                    logOut: function () {
                        localStorageService.remove(flag.WEBSTORE.AUTHORIZATION_DATA);

                        store.authentication.isAuth = false;
                        store.authentication.userName = "";
                    }                };

            self.saveRegistration = local.saveRegistration;
            self.login = local.login;
            self.logOut = local.logOut;

            return self;
        }
    ]);

}(window.angular));
