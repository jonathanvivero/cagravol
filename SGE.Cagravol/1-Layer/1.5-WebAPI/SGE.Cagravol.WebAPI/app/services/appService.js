(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.service('AppService', ['$window', '$rootScope', '$q', '$timeout', '$location', 'localStorageService', 'StoreFactory', 'I18nService', 'FlagService', '$sce', 'EntityService', 
        function (root, $rootScope, $q, $timeout, $location, localStorageService, store, i18n, flag, $sce, entity) {
            var self = this,
                local = {
                    isLoadingData: function () {
                        return store.loadingData;
                    },
                    endLoadingData: function () {
                        store.loadingData = false;                        
                    },
                    startFileUpload: function () {
                        store.loadingFile = true;
                    },
                    finishFileUpload: function () {
                        store.loadingFile = false;
                    },
                    isUploadingFile: function () {
                        return (store.loadingFile === true);
                    },
                    fillAuthData: function () {
                        var authData = localStorageService.get(flag.WEBSTORE.AUTHORIZATION_DATA);
                        if (authData) {
                            local.setAuthData(authData.userName, authData.token, authData.roleFlag, authData.name, authData.rpId);

                            store.recentProjectId = authData.rpId;
                        } else {
                            local.setAuthData();
                        }
                    },
                    startTimer: function (mseconds, cbFn) {
                        mseconds = mseconds || 1;
                        if (isNaN(mseconds)) {
                            mseconds = 1
                        }

                        var timer = $timeout(function () {
                            $timeout.cancel(timer);
                            cbFn();
                        }, mseconds);
                    },
                    getUrl: function (resource) {
                        resource = resource || '';
                        return store.serviceBase + resource;
                    },
                    getApiUrl: function (resource) {
                        resource = resource || '';
                        return store.serviceBase + 'api/' + resource;
                    },
                    setAuthData: function (userName, token, roleFlag, name, rpId) {
                        var isAuth = false;
                        userName = userName || '';
                        name = name || '';
                        roleFlag = roleFlag || '';
                        token = token || '';
                        rpId = parseInt(rpId || 0);

                        if (userName !== '' && token !== '') {
                            isAuth = true;
                        }

                        store.authentication.isAuth = isAuth;
                        store.authentication.userName = userName;
                        store.authentication.name = name;
                        store.authentication.roleFlag = roleFlag;
                        store.authentication.token = token;
                        store.authentication.rpId = rpId;

                        store.recentProjectId = rpId;
                    },
                    getRecentProjectId: function () {
                        return store.recentProjectId;
                    },
                    setRecentProjectId: function (id) {
                        store.recentProjectId = id || 0;
                    },
                    userIs: {
                        a: function(){
                            return (store.authentication.roleFlag.toUpperCase().indexOf('A') >= 0);
                        },
                        m: function(){
                            return (store.authentication.roleFlag.toUpperCase().indexOf('M') >= 0);
                        },
                        s: function(){
                            return (store.authentication.roleFlag.toUpperCase().indexOf('S') >= 0);
                        },
                        o: function(){
                            return (store.authentication.roleFlag.toUpperCase().indexOf('O') >= 0);
                        },
                        a_or_m: function(){
                            return (store.authentication.roleFlag.toUpperCase().indexOf('A') >= 0
                                || store.authentication.roleFlag.toUpperCase().indexOf('M') >= 0);
                        },
                        m_or_s: function(){
                            return (store.authentication.roleFlag.toUpperCase().indexOf('S') >= 0
                                || store.authentication.roleFlag.toUpperCase().indexOf('M') >= 0);
                        },
                        s_or_o: function(){
                            return (store.authentication.roleFlag.toUpperCase().indexOf('S') >= 0
                                || store.authentication.roleFlag.toUpperCase().indexOf('O') >= 0);
                        },
                        c: function(){
                            return (
                                store.authentication.roleFlag.toUpperCase().length === 1 
                                &&
                                store.authentication.roleFlag.toUpperCase().indexOf('C') === 0
                            );
                        }
                    },
                    getAuthData: function () {
                        return store.authentication;
                    },
                    getCurrentUserName: function () {
                        return store.authentication.userName;
                    },
                    persistAuthData: function () {
                        localStorageService.set(flag.WEBSTORE.AUTHORIZATION_DATA, store.authentication);
                    },
                    persistStateData: function () {
                        localStorageService.set(flag.WEBSTORE.STATE, store.state);
                    },
                    getI18n: function (mod, lang) {
                        lang = lang || store.defaultLang;
                        mod = mod || 'common';
                        var index = 0, 
                            modSplit = mod.split('.');

                        index = i18n.langListIndex[lang];

                        if (modSplit.length === 1) {
                            return i18n[mod][index];
                        } else if (modSplit.length === 2) {
                            return i18n[modSplit[0]][modSplit[1]][index];
                        } else {
                            return i18n[modSplit[0]][modSplit[1]][modSplit[2]][index];
                        }

                    },
                    go: {
                        to: function (res) {
                            store.loadingData = true;
                            root.console.info("loading Data = true for => " + res);
                            $location.path('/' + res);
                        },
                        login: function () {
                            local.go.to('login');
                        },
                        test:{
                            test: function () {
                                local.go.to('test');
                            } ,
                            sendEmail: function () {
                                local.go.to('test/send');
                            }
                        },
                        panel: function () {
                            local.go.to('panel');
                        },
                        home: function () {
                            local.go.to('home');
                        },
                        project: {
                            list:function(){
                                local.go.to('project/list');
                            },
                            create: function () {
                                local.go.to('project/create');
                            },
                            edit: function (params) {
                                local.go.to('project/edit/' + params);
                            },
                            activity: function (params) {
                                local.go.to('project/activity/' + params);
                            },
                            delete: function (params) {
                                local.go.to('project/delete/' + params);
                            },
                            gspace: {
                                list: function (params) {
                                    local.go.to('project/gspace/activity/' + params);
                                },                                
                                newfile: function () {
                                    local.go.to('project/gspace/newfile' + params);
                                },
                                edit: function (params) {
                                    local.go.to('project/gspace/file/' + params.pid + '/' + params.id);
                                },
                                file: function (params) {
                                    local.go.to('project/gspace/file/' + params.pid + '/' + params.id);
                                },
                                resend: function (params) {
                                    local.go.to('project/gspace/resendfile/' + params.pid + '/' + params.id);
                                },
                                resendfile: function (params) {
                                    local.go.to('project/gspace/resendfile/' + params.pid + '/' + params.id);
                                },
                                delete: function (params) {
                                    local.go.to('project/gspace/deletefile/' + params.pid + '/' + params.id);
                                },
                                deletefile: function (params) {
                                    local.go.to('project/gspace/deletefile/' + params.pid + '/' + params.id);
                                }
                            },
                            customer:{
                                activity: function (params) {
                                            if(params){
                                                local.go.to('project/customer/activity/' + params.pid + '/' + params.id);
                                            }else{
                                               local.go.to('panel');         
                                            }                                    
                                },
                                assignment: function (params) {
                                            if(params){
                                                local.go.to('project/customer/assignment/' + params.pid + '/' + params.id);                    
                                            }else{
                                               local.go.to('panel');         
                                            }
                                    
                                },
                            }
                        },
                        myspace: {
                            activity:function(){
                                local.go.to('myspace/activity');
                            },
                            newfile: function (params) {
                                local.go.to('myspace/newfile/' + params);
                            },
                            edit: function (params) {
                                local.go.to('myspace/file/' + params);
                            },
                            file: function (params) {
                                local.go.to('myspace/file/' + params);
                            },
                            resend: function (params) {
                                local.go.to('myspace/resendfile/' + params);
                            },
                            resendfile: function (params) {
                                local.go.to('myspace/resendfile/' + params);
                            },
                            delete: function (params) {
                                local.go.to('myspace/deletefile/' + params);
                            },
                            deletefile: function (params) {
                                local.go.to('myspace/deletefile/' + params);
                            }
                        },
                        file: {
                            history: function (params){
                                if (params.id){
                                    local.go.to('file/history/' + params.id + '/' + params.pid);
                                }else{
                                    local.go.to('file/history/' + params.id + '/');
                                }
                                
                            },
                        }
                    },
                    getUrl4Upload: function () {
                        return store.serviceBase + 'api/Upload';
                    },
                    setTemp: function (key, value) {                        
                        store.temp[key] = value;
                    },
                    getTemp: function (key, keepValue) {

                        var item = store.temp[key];
                        keepValue = keepValue || false;
                        
                        if (keepValue === false) {
                            delete store.temp[key];
                        }

                        return item;
                    },
                    getDefault: {
                        lang: function () {
                            return store.defaultLang;
                        },
                        milisecondsForRedirection: function () {
                            return store.defaultMilisecondsForRedirection;
                        }
                    },
                    setUserPanels: function (panels) {
                        store.userPanels = panels;
                    },
                    getUserPanels: function () {
                        return store.userPanels;
                    },
                    configNewRoutes: function (routes) { },
                    setScopeSettings: function ($scope, i18nArray) {

                        //For loading bars animation
                        $scope.waitForData = false;

                        $scope.wait = function () {
                            $scope.waitForData = true;
                        };

                        $scope.endWait = function () {
                            $scope.waitForData = false;
                        };

                        local.setScopeMessageSettings($scope);

                        //Internationalization - i18n                        
                        if (i18nArray) {
                            $scope.resx = {};
                            if (typeof (i18nArray) === typeof ([])) {
                                if (i18nArray.length > 0) {                                    
                                    $scope.resx = local.getI18n(i18nArray[0]) || {};
                                    
                                    for (var x = 1; x < i18nArray.length ; x++) {
                                        $scope.resx[i18nArray[x]] = local.getI18n(i18nArray[x]) || {};
                                    }
                                    $scope.resx.common = local.getI18n('common') || {};
                                }
                            } else if (typeof (i18nArray) === typeof ('')) {
                                $scope.resx = local.getI18n(i18nArray) || {};
                                $scope.resx.common = local.getI18n('common') || {};
                            }
                        }                        

                    },                    
                    setScopeMessageSettings: function ($scope) {
                        $scope.errorMessage = '';
                        $scope.message = '';

                        $scope.showError = function () {
                            return ($scope.errorMessage !== '');
                        };

                        $scope.showMessage = function () {
                            return ($scope.message !== '');
                        };

                        $scope.getErrorMessage = function () {
                            return $sce.trustAsHtml($scope.errorMessage);
                        };

                        $scope.getGeneralMessage = function () {
                            return $sce.trustAsHtml($scope.message);
                        };

                        $scope.showInfoBar = function () {
                            return ($scope.errorMessage !== '' || $scope.message !== '');
                        };

                        $scope.closeError = function () {
                            $scope.errorMessage = '';
                        };

                        $scope.closeMessage = function () {
                            $scope.message = '';
                        };

                    },
                    setDatePickerSettings: function (scope, fields) {
                        var x = 0,
                            list = [],
                            minDate = new Date(),
                            maxDate, lbl, 
                            resx = local.getI18n('common');


                        maxDate = new Date(minDate.getFullYear() + store.datePicker.yearsAfterMaxtDateLimit, 11, 31);
                        minDate = new Date(minDate.setDate(minDate.getDate() - store.datePicker.daysBeforeMinDateLimit))
                        
                        //if (typeof (fields) !== typeof (list)) {
                        //    list.push(fields.toString());
                        //} else {
                        //    list = fields;
                        //}

                        scope.dateOptions = {
                            dateDisabled: function (data) {
                                var date = data.date,
                                mode = data.mode;
                                return false; /*mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);*/
                            },                            
                            formatYear: 'yyyy',
                            maxDate: maxDate,
                            minDate: minDate,
                            startingDay: 1
                        };
                        scope.altInputFormats = store.datePicker.altInputFormats,
                        scope.dtFormat = store.datePicker.formats[store.datePicker.defaultFormatIndex];

                        scope.clearText = resx.clear;
                        scope.currentText = resx.today;
                        scope.closeText = resx.done;


                        //for (x = 0; x < list.length; x++) {
                        //    lbl = list[x];
                        //    scope['popup_' + lbl] = {opened: false};
                        //    scope['open_' + lbl] = function () {
                        //        scope['popup_' + lbl].opened = true;
                        //    };
                        //}
                    },
                    setState: function (state, params) {
                        store.state = {
                            last: state || 'home',
                            params: params || ''
                        };
                        local.persistStateData();                        
                    },                    
                    goByState: function () {

                        if (store.state === null || store.state === undefined){
                            store.state = localStorageService.get(flag.WEBSTORE.STATE) || { last: 'home', params: '' };
                        }

                        var st = store.state.last || defaultState || 'panel';
                        var stParam = store.state.params || '';

                        if (st === '') {
                            local.go.home();
                        } else {
                            switch (st) {
                                case 'project.list':
                                    local.go.project.list();
                                    break;
                                case 'project.edit':
                                    local.go.project.edit(stParam);
                                    break;
                                case 'project.create':
                                    local.go.project.create();
                                    break;
                                case 'project.delete':
                                    local.go.project.delete(stParam);
                                    break;
                                case 'project.activity':
                                    local.go.project.activity(stParam);
                                    break;
                                case 'project.gspace.activity':
                                    local.go.project.gspace.list(stParam);
                                    break;
                                case 'project.gspace.deletefile':
                                    local.go.project.gspace.delete(stParam);
                                    break;
                                case 'project.gspace.resendfile':
                                    local.go.project.gspace.resendfile(stParam);
                                    break;
                                case 'project.gspace.file':
                                    local.go.project.gspace.file(stParam);
                                    break;
                                case 'project.customer.activity':
                                    local.go.project.customer.activity(stParam);
                                    break;
                                case 'project.customer.assignment':
                                    local.go.project.customer.assignment(stParam);
                                    break;
                                case 'myspace.activity':
                                    local.go.myspace.activity(stParam);
                                    break;
                                case 'myspace.newfile':
                                    local.go.myspace.newfile(stParam);
                                    break;
                                case 'myspace.file':
                                    local.go.myspace.file(stParam);
                                    break;
                                case 'myspace.resendfile':
                                    local.go.myspace.resendfile(stParam);
                                    break;
                                case 'myspace.deletefile':
                                    local.go.myspace.deletefile(stParam);
                                    break;
                                case 'test.sendemail':
                                    local.go.test.sendEmail(stParam);
                                    break;
                                case 'file.history':
                                    local.go.file.history(stParam);
                                    break;
                                case 'panel':
                                    local.go.panel();
                                    break;
                                default:
                                    local.go.panel();
                                    break;
                            }
                        }
                    },
                    setDefaultPlatformParameters: function (params)
                    {
                        store.defaultPlatformParameters = new entity.DefaultPlatformParameters(params);

                    },
                    getPlatformParams: function () {
                        return store.defaultPlatformParameters;
                    },
                    addPromiseResolution: function (source, key) {
                        store.promises[source] = store.promises[source] || []
                        store.promises[source].push(key);
                    },
                    raisePromisesResolution: function (source, params) {
                        var arr;
                        for (var s in store.promises) {
                            if (store.promises.hasOwnProperty(s)
                                && source === s) {
                                arr = store.promises[s];
                                for (var x = 0; x < arr.length; x++) {
                                    $rootScope.$broadcast(arr[x], params);
                                }
                            }
                        }
                    },
                    getFileTypeList: function () {
                        //return store.defaultPlatformParameters.fileTypes;
                        return store.fileTypeList;
                    },
                    resetUploadingFile: function () {
                        store.uploadingFile = null;
                    },
                    isAbleWFMovement: function (wfCode) {
                        return (wfCode === flag.WFSTATES.FILE_REJECTED);
                    },
                    getAbleWFMovements: function (wfCode, projectId) {
                        projectId = projectId || 0;

                        var res = [];
                        switch (wfCode) {
                            case flag.WFSTATES.FILE_FILED:
                                res = [];
                                break;
                            case flag.WFSTATES.FILE_IN_PRODUCTION:
                                res = [flag.WFSTATES.FILE_REJECTED, flag.WFSTATES.FILE_FILED];
                                break;
                            case flag.WFSTATES.FILE_IN_REVISION:
                                res = [flag.WFSTATES.FILE_REJECTED, flag.WFSTATES.FILE_READY_FOR_PRODUCTION];
                                break;
                            case flag.WFSTATES.FILE_IN_UPLOAD:
                                res = [];
                                break;
                            case flag.WFSTATES.FILE_RE_UPLOADED:
                                res = [flag.WFSTATES.FILE_REJECTED, flag.WFSTATES.FILE_IN_REVISION];
                                break;
                            case flag.WFSTATES.FILE_LOADED:
                                res = [flag.WFSTATES.FILE_REJECTED, flag.WFSTATES.FILE_IN_PRODUCTION];
                                break;
                            case flag.WFSTATES.FILE_READY_FOR_PRODUCTION:
                                res = [flag.WFSTATES.FILE_REJECTED, flag.WFSTATES.FILE_IN_PRODUCTION];
                                break;
                            case flag.WFSTATES.FILE_REJECTED:
                                res = ['', flag.WFSTATES.FILE_FILED];
                                break;
                            case flag.WFSTATES.FILE_UPLOAD_FAILED:
                                res = ['', flag.WFSTATES.FILE_FILED];
                                break;
                            default:
                                res = [];
                                break;

                        }
                        return res;
                    },
                    getAbleWFGSMovements: function (wfCode, projectId) {
                        projectId = projectId || 0;

                        var res = [];
                        switch (wfCode) {
                            case flag.WFSTATES.FILE_IN_PRODUCTION:
                                res = ['', flag.WFSTATES.FILE_FILED];
                                break;
                            default:
                                res = [];
                                break;
                        }
                        return res;
                    },
                };

            self.getAbleWFMovements = local.getAbleWFMovements;
            self.getAbleWFGSMovements = local.getAbleWFGSMovements;
            self.isAbleWFMovement = local.isAbleWFMovement;
            self.fillAuthData = local.fillAuthData;
            self.startTimer = local.startTimer;
            self.getUrl = local.getUrl;
            self.getUrl4Upload = local.getUrl4Upload;
            self.getApiUrl = local.getApiUrl;
            self.setAuthData = local.setAuthData;
            self.getAuthData = local.getAuthData;
            self.getCurrentUserName = local.getCurrentUserName;
            self.userIs = local.userIs;
            self.getI18n = local.getI18n;
            self.persistAuthData = local.persistAuthData;
            self.go = local.go;
            self.getTemp = local.getTemp;
            self.setTemp = local.setTemp;
            self.getDefault = local.getDefault;
            self.getUserPanels = local.getUserPanels;
            self.setUserPanels = local.setUserPanels;
            self.configNewRoutes = local.configNewRoutes;
            self.setScopeMessageSettings = local.setScopeMessageSettings;
            self.setDatePickerSettings = local.setDatePickerSettings;
            self.getState = local.getState;
            self.setState = local.setState;
            self.goByState = local.goByState;
            self.setDefaultPlatformParameters = local.setDefaultPlatformParameters;
            self.getPlatformParams = local.getPlatformParams;
            self.addPromiseResolution = local.addPromiseResolution;
            self.raisePromisesResolution = local.raisePromisesResolution;
            self.isLoadingData = local.isLoadingData;
            self.endLoadingData = local.endLoadingData;
            self.startFileUpload = local.startFileUpload;
            self.finishFileUpload = local.finishFileUpload;
            self.setScopeSettings = local.setScopeSettings;
            self.getFileTypeList = local.getFileTypeList;
            self.resetUploadingFile = local.resetUploadingFile;
            self.isUploadingFile = local.isUploadingFile;
            self.getRecentProjectId = local.getRecentProjectId;
            self.setRecentProjectId = local.setRecentProjectId;

            return self;
        }
    ]);

}(window.angular));
