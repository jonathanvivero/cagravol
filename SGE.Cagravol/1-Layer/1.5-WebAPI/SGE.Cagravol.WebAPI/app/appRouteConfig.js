(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.config(['$compileProvider', '$routeProvider',
        function ($compileProvider, $routeProvider) {

            $routeProvider.when("/home", {
                controller: "HomeController",
                templateUrl: "/app/views/home.html"
            });

            $routeProvider.when("/login", {
                controller: "LoginController",
                templateUrl: "/app/views/account/login.html"
            });

            $routeProvider.when("/login/:email", {
                controller: "LoginController",
                templateUrl: "/app/views/account/login.html"
            });

            $routeProvider.when("/rsignup/:cid", {
                controller: "SignUpReservationController",
                templateUrl: "/app/views/account/rsignup.html"
            });

            $routeProvider.when("/signup", {
                controller: "SignUpController",
                templateUrl: "/app/views/account/signup.html"
            });

            $routeProvider.when("/panel", {
                controller: "PanelController",
                templateUrl: "/app/views/panels/panel.html"
            });

            $routeProvider.when("/test", {
                controller: "TestController",
                templateUrl: "/app/views/test/test.html"
            });

            $routeProvider.when("/test/send", {
                controller: "TestSendEmailController",
                templateUrl: "/app/views/test/sendEmail.html"
            });

            /*DYNAMICS ROUTES*/

            $routeProvider.when("/files", {
                controller: "CustomerFileListController",
                templateUrl: "/app/views/customer/filelist.html"
            });

            $routeProvider.when("/file/history/:id/:pid", {
                controller: "FileHistoryController",
                templateUrl: "/app/views/file/history.html"
            });


            /*Project Routes*/
            $routeProvider.when("/project/create", {
                controller: "EditProjectController",
                templateUrl: "/app/views/projects/edit.html"
            });

            $routeProvider.when("/project/list", {
                controller: "ListProjectController",
                templateUrl: "/app/views/projects/list.html"
            });

            $routeProvider.when("/project", {
                controller: "ListProjectController",
                templateUrl: "/app/views/projects/list.html"
            });

            $routeProvider.when("/project/manage/:id", {
                controller: "ManageProjectController",
                templateUrl: "/app/views/projects/manage.html"
            });

            $routeProvider.when("/project/edit/:id", {
                controller: "EditProjectController",
                templateUrl: "/app/views/projects/edit.html"
            });

            $routeProvider.when("/project/delete/:id", {
                controller: "DeleteProjectController",
                templateUrl: "/app/views/projects/delete.html"
            });

            $routeProvider.when("/project/activity/:id", {
                controller: "ProjectActivityController",
                templateUrl: "/app/views/projects/activity.html"
            });

            $routeProvider.when("/project/customer/activity/:pid/:id", {
                controller: "ProjectCustomerActivityController",
                templateUrl: "/app/views/projects/customer-activity.html"
            });

            $routeProvider.when("/project/customer/assignment/:pid/:id", {
                controller: "ProjectCustomerAssignmentController",
                templateUrl: "/app/views/projects/customer-assignment.html"
            });

            $routeProvider.when("/project/gspace/activity/:id", {
                controller: "ProjectGeneralSpaceActivityController",
                templateUrl: "/app/views/projects/general-space-activity.html"
            });
            
            $routeProvider.when("/project/gspace/newfile/:pid", {
                controller: "ProjectGeneralSpaceNewFileController",
                templateUrl: "/app/views/myspace/new.html"
            });

            $routeProvider.when("/project/gspace/file/:pid/:id", {
                controller: "ProjectGeneralSpaceFileController",
                templateUrl: "/app/views/myspace/edit.html"
            });

            $routeProvider.when("/project/gspace/resendfile/:pid/:id", {
                controller: "ProjectGeneralSpaceResendFileController",
                templateUrl: "/app/views/myspace/resend.html"
            });

            $routeProvider.when("/project/gspace/deletefile/:pid/:id", {
                controller: "ProjectGeneralSpaceDeleteFileController",
                templateUrl: "/app/views/myspace/delete.html"
            });

            /*Customer My Space Routes*/

            $routeProvider.when("/myspace", {
                controller: "MySpaceActivityController",
                templateUrl: "/app/views/myspace/activity.html"
            });

            $routeProvider.when("/myspace/list", {
                controller: "MySpaceActivityController",
                templateUrl: "/app/views/myspace/activity.html"
            });

            $routeProvider.when("/myspace/activity", {
                controller: "MySpaceActivityController",
                templateUrl: "/app/views/myspace/activity.html"
            });

            $routeProvider.when("/myspace/newfile/:pid", {
                controller: "MySpaceNewFileController",
                templateUrl: "/app/views/myspace/new.html"
            });

            $routeProvider.when("/myspace/file/:id", {
                controller: "MySpaceFileController",
                templateUrl: "/app/views/myspace/edit.html"
            });

            $routeProvider.when("/myspace/resendfile/:id", {
                controller: "MySpaceResendFileController",
                templateUrl: "/app/views/myspace/resend.html"
            });

            $routeProvider.when("/myspace/deletefile/:id", {
                controller: "MySpaceDeleteFileController",
                templateUrl: "/app/views/myspace/delete.html"
            });

            $routeProvider.when("/myspace/profile", {
                controller: "MySpaceProfileFileController",
                templateUrl: "/app/views/myspace/profile.html"
            });

            /*END OF DYNAMIC ROUTES*/

            $routeProvider.otherwise({ redirectTo: "/panel" });

            $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|chrome-extension|tel|geo|sms):/);
        }
    ]);
}(window.angular));
