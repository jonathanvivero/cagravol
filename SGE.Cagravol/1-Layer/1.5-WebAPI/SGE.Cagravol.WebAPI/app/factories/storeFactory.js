(function (angular) {
    var sge = angular.module('sgeApp')
    'use strict';
    sge.factory('StoreFactory', [function () {

        var self = this,
            local = {
                loadingData: false,
                loadingFile: false,
                serviceBase: 'http://localhost:52643/',
                serviceBaseProd: 'http://grafidec.test.sgesoft.com/',
                authentication: {
                    isAuth: false,
                    userName: "",
                    name: "",
                    roleFlag: "",
                    token: "",
                    rpId: 0
                },
                fileTypeList: [                    
                    { id: 1, name: 'JPEG', notes: '', fileExtensions: ['jpg', 'jpeg', 'jpe', 'jfif'] },
                    { id: 2, name: 'PNG', notes: '', fileExtensions: ['png'] },
                    { id: 3, name: 'Adobe Photoshop', notes: '', fileExtensions: ['psd'] },
                    { id: 4, name: 'Adobe Illustrator', notes: '', fileExtensions: ['ai'] },
                    { id: 5, name: 'SVG', notes: '', fileExtensions: ['svg', 'svgz'] },
                    { id: 6, name: 'PDF', notes: '', fileExtensions: ['pdf'] },
                    { id: 7, name: 'BMP', notes: '', fileExtensions: ['bmp', 'dib'] },
                    { id: 8, name: 'WMF', notes: 'Windows Meta File', fileExtensions: ['wmf'] },
                    { id: 9, name: 'GIF', notes: '', fileExtensions: ['gif', 'giff'] },
                    { id: 10, name: 'TIFF', notes: '', fileExtensions: ['tif', 'tiff', 'tff'] },
                    { id: 11, name: 'TGA', notes: '', fileExtensions: ['tga'] },
                    { id: 12, name: 'Corel Draw', notes: '', fileExtensions: ['cdr'] },
                    { id: 13, name: 'Corel Photo Paint', notes: '', fileExtensions: ['ctp'] },
                    { id: 14, name: 'EPS', notes: '', fileExtensions: ['eps'] },
                    { id: 15, name: 'ZIP', notes: '', fileExtensions: ['zip'] },
                    { id: 16, name: 'RAR', notes: '', fileExtensions: ['rar', 'r00'] },
                ],
                defaultPlatformParameters: null,
                defaultLang: 'es_ES',
                defaultMilisecondsForRedirection: 5000,
                recentProjectId: 0,
                userPanels: null,
                datePicker:{
                    formats: ['dd/MM/yyyy', 'dd/MMMM/yyyy', 'dd/MM/yy', 'dd/MMMM/yy', 'shortDate'],
                    defaultFormatIndex: 0,
                    altInputFormats: ['d!/M!/yyyy', 'd!/M!/yy'],
                    yearsAfterMaxtDateLimit: 2,
                    daysBeforeMinDateLimit: 30
                },
                state: null,
                temp: {},
                promises: {},
                uploadingFile:null
            };

        self.serviceBase = local.serviceBase;
        self.loadingData = local.loadingData;
        self.authentication = local.authentication;
        self.defaultLang = local.defaultLang;
        self.defaultPlatformParameters = local.defaultPlatformParameters;
        self.defaultMilisecondsForRedirection = local.defaultMilisecondsForRedirection;
        self.userPanels = local.userPanels;
        self.temp = local.temp;
        self.datePicker = local.datePicker;
        self.state = local.state;
        self.promises = local.promises;
        self.fileTypeList = local.fileTypeList;
        self.loadingFile = local.loadingFile;
        self.uploadingFile = local.uploadingFile;

        return self;
    }]);
}(window.angular));
