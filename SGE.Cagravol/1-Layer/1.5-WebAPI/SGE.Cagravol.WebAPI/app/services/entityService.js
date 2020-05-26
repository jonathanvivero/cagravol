(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.service('EntityService', [
        function () {

            var self = this,
                local = {
                    File: function (item) {
                        item = item || {};
                        var _self = this,
                            _local = {
                                id: item.id || 0,
                                customerId: item.customerId || 0,
                                fileTypeId: item.fileTypeId || 1,
                                name: item.name || '',
                                url: item.url || '',
                                version: item.version || 1,
                                firstDeliveryDate: new Date(item.firstDeliveryDate || new Date()),
                                fileName: item.fileName || '',
                                size: item.size || 0,
                                mimeType: item.mimeType || '',
                                channelId: item.channelId || ''
                            };

                        _self.id = _local.id;
                        _self.customerId = _local.customerId;
                        _self.fileTypeId = _local.fileTypeId;
                        _self.name = _local.name;
                        _self.url = _local.url;
                        _self.version = _local.version;
                        _self.firstDeliveryDate = local.firstDeliveryDate;
                        _self.fileName = _local.fileName;
                        _self.size = _local.size;
                        _self.mimeType = _local.mimeType;
                        _self.channelId = _local.channelId;

                        return _self;
                    },
                    FileReader: function () {
                        var _self = this,
                            _local = {
                                reader: new FileReader(),
                                index: 0,
                                bufferSize: 262144,
                                totalFileSize: 0,
                                channelId: '',
                                progress: 0.0,
                                parts: 0,
                                startTime: new Date(),
                                estimatedFinalTime: new Date()
                            };

                        _self.reader = _local.reader;
                        _self.index = _local.index;
                        _self.bufferSize = _local.bufferSize;
                        _self.totalFileSize = _local.totalFileSize;
                        _self.channelId = _local.channelId;
                        _self.progress = _local.progress;
                        _self.parts = _local.parts;
                        _self.startTime = _local.startTime;
                        _self.estimatedFinalTime = _local.estimatedFinalTime;

                        return _self;
                    },
                    MenuItem: function (name, fn, fnParamName, condition) {
                        var _self = this,
                            _local = {
                                name: name || '',
                                fn: fn || function () { },
                                fnParamName: fnParamName || null,
                                condition: condition || true
                            };

                        _self.name = _local.name;
                        _self.fn = _local.fn;
                        _self.fnParamName = _local.fnParamName;
                        _self.condition = _local.condition;

                        return _self;
                    },
                    Menu: function (initialMenu) {
                        var _self = this,
                            _local = {
                                menuItems: [],
                                getMenuItems: function () {
                                    return _local.menuItems || [];
                                },
                                addMenuItem: function (mItem) {
                                    if (typeof (mItem) === typeof (new local.MenuItem())) {
                                        _local.menuItems.push(mItem);
                                    } else {
                                        _local.menuItems.push(new local.MenuItem(mItem.name, mItem.fn, mItem.fnParamName, mItem.condition));
                                    }
                                }
                            };

                        if (initialMenu) {
                            _local.addMenuItem(initialMenu);
                        }

                        _self.getMenuItems = _local.getMenuItems;
                        _self.addMenuItem = _local.addMenuItem;

                        return _self;
                    },
                    PanelItem: function (title, url) {
                        var _self = this,
                            _local = {
                                title: title || '',
                                url: url || 'empty.html'
                            };

                        _self.title = _local.title;
                        _self.url = _local.url;

                        return _self;
                    },
                    PanelRow: function (_show, leftTitle, leftUrl, centerTitle, centerUrl, rightTitle, rightUrl) {
                        var _self = this,
                            _local = {
                                _show: _show || false,
                                show: function () {
                                    return _local.show;
                                },
                                left: new local.PanelItem(leftTitle, leftUrl),
                                center: new local.PanelItem(centerTitle, centerUrl),
                                right: new local.PanelItem(rightTitle, rightUrl)
                            };

                        _self.show = _local.show;
                        _self.left = _local.left;
                        _self.center = _local.center;
                        _self.right = _local.right;

                        return _self;
                    },
                    Project: function (params) {

                        params = params || {};

                        var _self = this,
                            _local = {
                                id: params.id || params.Id || 0,
                                name: params.name || params.Name || '',
                                code: params.code || params.Code || '',
                                description: params.description || params.Description || '',
                                startDate: new Date(params.startDate || params.StartDate || new Date()),
                                finishDate: new Date(params.finishDate || params.FinishDate || new Date()),
                                extraChargeForSendingDate: new Date(params.extraChargeForSendingDate || params.ExtraChargeForSendingDate || new Date()),
                                limitForSendingDate: new Date(params.limitForSendingDate || params.LimitForSendingDate || new Date()),
                                extraChargePercentage: params.extraChargePercentage || params.ExtraChargePercentage || 0.00,
                                notes: params.notes || params.Notes || '',
                                totalStands: params.totalStands || params.TotalStands || 1
                            };

                        _self.id = _local.id;
                        _self.name = _local.name;
                        _self.code = _local.code;
                        _self.description = _local.description;
                        _self.startDate = _local.startDate;
                        _self.finishDate = _local.finishDate;
                        _self.extraChargeForSendingDate = _local.extraChargeForSendingDate;
                        _self.limitForSendingDate = _local.limitForSendingDate;
                        _self.extraChargePercentage = _local.extraChargePercentage;
                        _self.notes = _local.notes;
                        _self.totalStands = _local.totalStands;

                        return _self;
                    },
                    DefaultPlatformParameters: function (data) {
                       data = data || {};

                        var _self = this,
                            _local = {
                                totalStands: data.totalStands || 1,
                                publicKey: data.publicKey || ''
                            };

                        _self.totalStands = _local.totalStands;
                        _self.publicKey = _local.publicKey;

                        return _self; 
                    },                    
                    FileHistory: function (fh) {
                        
                        var data = fh || {},
                        states = data.states || [];

                        var _self = this,
                            _local = {
                                file: new local.File(data.file),
                                states: data.states
                            };

                        //for (var i = 0; i < length; i++) {
                        //    if (states[i]) {
                        //        _local.states.push(new local.FileWFState(state[i]))
                        //    }
                        //}

                        _self.file = _local.file;
                        _self.states = _local.states;

                        return _self;
                    }
                };

            self.PanelItem = local.PanelItem;
            self.PanelRow = local.PanelRow;
            self.Project = local.Project;
            self.DefaultPlatformParameters = local.DefaultPlatformParameters;
            self.File = local.File;
            self.FileReader = local.FileReader;
            self.FileHistory = local.FileHistory;

            return self;
        }
    ]);

}(window.angular));