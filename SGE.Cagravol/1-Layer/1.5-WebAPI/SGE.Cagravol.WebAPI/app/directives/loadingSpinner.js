
(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.directive('loadingSpinner', ['$http', 'StoreFactory',
        function ($http, store) {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs) {
                scope.isLoading = function () {
                    var reqLen = ($http.pendingRequests.length > 0) || false;

                    //reqLen = reqLen || (store.loadingData === true);
                    //console.log({loading: store.loadingData, reqs:$http.pendingRequests, scope: scope, elm: elm, attrs: attrs });
                    //($http.pendingRequests.length > 0 || store.loadingData === true) || false;

                    //When loading files, loading spinner should not appear
                    if (store.loadingFile === true) {
                        return false;
                    } else {
                        return reqLen;
                    }


                };

                scope.$watch(scope.isLoading, function (v) {
                    if (v) {
                        if (elm.hasClass('lshidden')){
                            elm.removeClass('lshidden');
                            store.loadingData = true;
                        }
                    } else {
                        if (!elm.hasClass('lshidden')) {
                            elm.addClass('lshidden');
                            store.loadingData = false;
                        }
                        
                    }
                });
            }
        };
    }]);
}(window.angular));


