(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.service('NetService', ['$window', '$http', '$q', '$timeout', 'StoreFactory',
        function (root, $http, $q, $timeout, store) {
            var self = this,
                local = {
                    GET: function (url, data) {

                        return $http({
                            method: 'GET',
                            url: url,
                            params: data
                        });
                    },
                    POST: function (url, data) {
                        return $http({
                            method: 'POST',
                            url: url,
                            data: data
                        });
                    },
                    POSTBSON: function (url, data) {
                        return $http({
                            method: 'POST',
                            url: url,
                            config: {
                                headers: { 'Content-Type': 'application/bson' }
                            },
                            data: data
                        });
                    },
                    PUT: function (url, data) {
                        return $http({
                            method: 'PUT',
                            url: url,
                            data: data
                        });
                    },
                    DELETE: function (url, data) {
                        return $http({
                            method: 'DELETE',
                            url: url,
                            params: data
                        });
                    },
                    _get: {
                        test: {
                            test: function (data) {
                                var url = store.serviceBase + 'api/Test';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                            sendEmail: function (data) {
                                var url = store.serviceBase + 'api/Alfred/SendEmail';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                        },
                        signup: {
                            spaceInfo: function (cid) {
                                var url = store.serviceBase + 'api/Alfred/SpaceInfo';
                                var data = {
                                    id: cid
                                };                                

                                return local.GET(url, data);
                            },                            
                        },
                        panels: function (data) {
                            var url = store.serviceBase + 'api/Panels';
                            data = data || {};
                            data.userName = store.authentication.userName;

                            return local.GET(url, data);
                        },
                        project: {
                            files: function (data) {
                                var url = store.serviceBase + 'api/Files';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                            list: function (data) {
                                var url = store.serviceBase + 'api/Project';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                            item: function (data) {
                                var url = store.serviceBase + 'api/Project/Item';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                            customerActivity: function (data) {
                                var url = store.serviceBase + 'api/Project/CustomerActivity';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                            gspace: {
                                list: function (data) {
                                    var url = store.serviceBase + 'api/Project/GSActivity';
                                    data = data || {};
                                    data.userName = store.authentication.userName;

                                    return local.GET(url, data);
                                },
                                item: function (data) {
                                    var url = store.serviceBase + 'api/Project/GSItem';
                                    data = data || {};
                                    data.userName = store.authentication.userName;

                                    return local.GET(url, data);
                                },
                            },
                        },
                        defaultPlatformParameters: function (data) {
                            var url = store.serviceBase + 'api/Alfred/DefaultPlatformParameters';
                            data = data || {};
                            data.userName = store.authentication.userName;

                            return local.GET(url, data);
                        },
                        myspace: {
                            list: function (data) {
                                var url = store.serviceBase + 'api/Customer/Activity';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                            item: function (data) {
                                var url = store.serviceBase + 'api/Customer/Item';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },                            

                        },
                        file: {
                            history: function (data) {
                                var url = store.serviceBase + 'api/File/History';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                            item: function (data) {
                                var url = store.serviceBase + 'api/File/Item';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.GET(url, data);
                            },
                        },

                    },
                    _post: {
                        registration: function (data) {
                            var url = 'api/Account/CustomerRegister';
                            data = data || {};
                            //data.userName = data.userName || store.authentication.userName;

                            return local.POST(url, data);
                        },
                        registerReservation: function (data) {
                            var url = 'api/Account/RegisterReservation';
                            data = data || {};
                            //data.userName = data.userName || store.authentication.userName;

                            return local.POST(url, data);
                        },
                        projectFiles: function (data) {
                            var url = store.serviceBase + 'api/Files';
                            data = data || {};
                            data.userName = store.authentication.userName;

                            return local.POST(url, data);
                        },
                        project:{
                            item: function (data) {
                                var url = store.serviceBase + 'api/Project';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                            gspace: {
                                edit: function (data) {
                                    var url = store.serviceBase + 'api/Project/GSUpdateItem';
                                    data = data || {};
                                    data.userName = store.authentication.userName;

                                    return local.POST(url, data);
                                },
                            },
                            excel: function (data) {
                                var url = store.serviceBase + 'api/Project/Excel';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                            space: function (data) {
                                var url = store.serviceBase + 'api/Project/ChangeSpaceStatus';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                        },
                        file: {
                            init: function (data) {
                                var url = store.serviceBase + 'api/Upload/Init';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                            end: function (data) {
                                var url = store.serviceBase + 'api/Upload/End';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                            hash: function (data) {
                                var url = store.serviceBase + 'api/Upload/Hash';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                            cancel: function (data) {
                                var url = store.serviceBase + 'api/Upload/Cancel';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                            addCommentToState: function (data) {
                                var url = store.serviceBase + 'api/File/AddCommentToState';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                        },                        
                        myspace: {
                            item: function (data) {
                                var url = store.serviceBase + 'api/Customer/Item';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                            edit: function (data) {
                                var url = store.serviceBase + 'api/Customer/UpdateFile';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            },
                        },
                        WF: {
                            move: function (data) {
                                var url = store.serviceBase + 'api/Workflow/MoveFile';
                                data = data || {};
                                data.userName = store.authentication.userName;

                                return local.POST(url, data);
                            }
                        },
                    },
                    _postbin: {
                        fileHash: function (data) {
                            var url = store.serviceBase + 'api/Upload/Hash';
                            data = data || {};
                            data.userName = store.authentication.userName;

                            return local.POSTBSON(url, data);
                        }
                    },
                    _delete: {
                        project: function (data) {
                            var url = store.serviceBase + 'api/Project/Delete';
                            data = data || {};
                            data.userName = store.authentication.userName;

                            return local.DELETE(url, data);
                        },
                        myspace: function (data) {
                            var url = store.serviceBase + 'api/Customer/Delete';
                            data = data || {};
                            data.userName = store.authentication.userName;

                            return local.DELETE(url, data);
                        },
                        gspace: function (data) {
                            var url = store.serviceBase + 'api/Project/GSDelete';
                            data = data || {};
                            data.userName = store.authentication.userName;

                            return local.DELETE(url, data);
                        },
                    },
                    _put: {
                    },
                    token: function (userName, password) {

                        var data = "grant_type=password&username=" + userName + "&password=" + password;

                        var q = $q.defer();

                        $http({
                            method: 'POST',
                            url: store.serviceBase + 'token',
                            data: data,
                            config: {
                                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                            }
                        }).success(function (response) {
                            q.resolve(response);
                        }).error(function (error) {
                            q.reject(error);
                        });

                        return q.promise;
                    },
                    getPendingRequests: function () {
                        return $http.pendingRequests.length;
                    }
                };

            self.GET = local.GET;
            self.POST = local.POST;
            self.POSTBSON = local.POSTBSON;
            self.PUT = local.PUT;
            self.DELETE = local.DELETE;
            self.get = local._get;
            self.post = local._post;
            self.postbin = local._postbin;
            self.put = local._put;
            self.delete = local._delete;
            self.token = local.token;
            self.getPendingRequests = local.getPendingRequests;
            return self;
        }
    ]);

}(window.angular));
