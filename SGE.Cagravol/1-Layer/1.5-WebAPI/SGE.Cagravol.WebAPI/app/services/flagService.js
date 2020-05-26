(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.service('FlagService', [
        function () {
            var self = this,
                local = {
                    TEMP: {
                        JUST_SIGNED_UP: 'JUST_SIGNED_UP',
                        PANELS: 'PANELS',
                        PROJECT_LIST_MESSAGE: 'PROJECT.LIST.MESSAGE',
                        MYSPACE_ACTIVITY_MESSAGE: 'MYSPACE_ACTIVITY.MESSAGE',
                        PROJECT_CUSTOMER_ACTIVITY_MESSAGE: 'PROJECT_CUSTOMER_ACTIVITY_MESSAGE',
                        DELETE_ITEM: 'DELETE_ITEM',
                        ACTIVITY_ITEM: 'ACTIVITY_ITEM',
                        EDIT_ITEM: 'EDIT_ITEM',
                        DELETE_ID: 'DELETE_ID',
                        FILE: 'FILE',
                        BACKTO_URL:'BACKTO_URL',
                    },
                    WEBSTORE: {
                        AUTHORIZATION_DATA: 'authorizationData',
                        STATE: 'stateData'
                    },
                    ERROR_CODES: {
                        VALIDATION_ERROR: "VALIDATION_ERROR",
                        USER_NOT_VALID: "USER_NOT_VALID",
                        INSUFFICIENT_PRIVILEGES: "INSUFFICIENT_PRIVILEGES",
                        ITEM_DOES_NOT_EXIST: "ITEM_DOES_NOT_EXIST",
                        CUSTOMER_USER_NOT_FOUND_FOR_ANY_PROJECT: "CUSTOMER_USER_NOT_FOUND_FOR_ANY_PROJECT",
                        FILE_NOT_FOUND: "FILE_NOT_FOUND",
                        CUSTOMER_NOT_FOUND: "CUSTOMER_NOT_FOUND",
                        PROJECT_NOT_FOUND: "PROJECT_NOT_FOUND",
                        WORKFLOW_NOT_FOUND: "WORKFLOW_NOT_FOUND",
                        STATE_NOT_FOUND: "STATE_NOT_FOUND",
                        TRANSITION_NOT_FOUND: "TRANSITION_NOT_FOUND",
                        ZERO_PROJECTS:"ZERO_PROJECTS",
                    },
                    BROADCAST_SOURCES: {
                        DEFAULT_PARAMS: "recieved::defaultParams"
                    },
                    BROADCAST_KEYS: {
                        SET_DEFAULT_PLATFORM_PARAMS: "set::default::platform::params"
                    },
                    CUSTOMER_SPACE_STATUS: {
                        FREE: 0,
                        RESERVED: 1,
                        ASSIGNED: 2
                    },
                    WFSTATES: {
                        FILE_FILED: 'FILE_FILED',
                        FILE_IN_PRODUCTION: 'FILE_IN_PRODUCTION',
                        FILE_IN_REVISION: 'FILE_IN_REVISION',
                        FILE_IN_UPLOAD: 'FILE_IN_UPLOAD',
                        FILE_RE_UPLOADED: 'FILE_RE_UPLOADED',
                        FILE_LOADED: 'FILE_LOADED',
                        FILE_READY_FOR_PRODUCTION: 'FILE_READY_FOR_PRODUCTION',
                        FILE_REJECTED: 'FILE_REJECTED',
                        FILE_UPLOAD_FAILED: 'FILE_UPLOAD_FAILED',
                    }
                };

            self.TEMP = local.TEMP;
            self.WEBSTORE = local.WEBSTORE;
            self.ERROR_CODES = local.ERROR_CODES;
            self.BROADCAST_SOURCES = local.BROADCAST_SOURCES;
            self.BROADCAST_KEYS = local.BROADCAST_KEYS;
            self.WFSTATES = local.WFSTATES;

            return self;
        }
    ]);

}(window.angular));
