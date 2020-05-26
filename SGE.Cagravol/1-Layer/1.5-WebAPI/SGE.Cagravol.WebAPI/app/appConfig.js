(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.config(['$httpProvider',
            function ($httpProvider) {                
                $httpProvider.interceptors.push('AuthInterceptorFactory');
            }
    ]);

    sge.run(['AppService','$rootScope', '$location',
        function (app, $rootScope, $location) {
            app.fillAuthData();
            app.resetUploadingFile();

            var authData = app.getAuthData() || {isAuth: false};
            if (authData.isAuth == true){
                app.goByState();
                $rootScope.$broadcast('auth-on');
            } else {                
                var url = $location.url();

                if (url.lastIndexOf('/rsignup/') === -1
                    && url.lastIndexOf('/login') === -1
                    && url.lastIndexOf('/signup') === -1) {
                        app.go.home();
                        $rootScope.$broadcast('auth-off');
                }

                
            }
        }
    ]);

}(window.angular));
