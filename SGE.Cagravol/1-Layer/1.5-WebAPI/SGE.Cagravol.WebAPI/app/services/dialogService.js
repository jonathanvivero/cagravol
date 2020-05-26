(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.service('DialogService', ['$log', '$q', 'FlagService', 'UtilService',
        function ($log, $q, flag, util) {

            var self = this,
                local = {
                    nullFn: function () { },
                    resx: {},
                    alert: function (title, message) {
                        var q = $q.defer();

                        return q.promise;
                    },
                    confirm: function (title, message, ok, cancel) {
                        var q = $q.defer();
                        var okTitle = ok[0] || '',
                            cancelTitle = cancel[0] || '',
                            okFn = ok[1] || local.nullFn,
                            cancelFn = cancel[1] || local.nullFn;


                        return q.promise;
                    },
                    dialog: function (title, message, options) {
                        var q = $q.defer();



                        return q.promise;
                    }
                };


            self.alert = local.alert;
            self.confirm = local.confirm;
            self.dialog = local.dialog;

            return self;
        }
    ]);

}(window.angular));
