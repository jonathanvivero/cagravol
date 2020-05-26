(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.directive('waitingSpinner', ['$http', 'StoreFactory',
        function ($http, store) {
            return {
                restrict: 'E',
                replace : true,
                template: "<div class='waitinging-spiner-holder'><div class='spinner'><div class='rect1'></div><div class='rect2'></div><div class='rect3'></div><div class='rect4'></div><div class='rect5'></div></div></div>",
                scope: false,
                link: function (scope, elm, attrs) {
                    scope.isWaiting = function () {
                        return ((scope.waitForData || false) === true);                        
                    };

                    scope.$watch(scope.isWaiting, function (v) {
                        if (v) {
                            if (elm.hasClass('lshidden')) {
                                elm.removeClass('lshidden');                                
                            }
                        } else {
                            if (!elm.hasClass('lshidden')) {
                                elm.addClass('lshidden');
                            }
                        }
                    });
                }
            };
        }]);
}(window.angular));