(function (window) {
    'use strict';
    var sge = window.angular.module('sgeApp');

    sge.service('UtilService', [
        function () {
            var self = this,
                local = {
                    pad: function (num, size) {
                        num = num || 0;
                        size = size || 2;

                        var s = "0" + num;
                        return s.substr(s.length - size);
                    },
                    arrayBufferToBase64: function (buffer) {
                        var binary = '';
                        var bytes = new Uint8Array(buffer);
                        var len = bytes.byteLength;
                        for (var i = 0; i < len; i++) {
                            binary += String.fromCharCode(bytes[i]);
                        }
                        return window.btoa(binary);
                    },
                    getByteSize: function (size) {

                        if (size === 0)
                            return '0 bytes';

                        var coefKb = 1024,
                            coefMb = (1024 * 1024),
                            coefGb = (1024 * 1024 * 1024),
                            sufix = " Gb",
                            humanSize = 0.0;

                        if (size > coefGb) {
                            humanSize = Math.round(size * 100 / coefGb) / 100;
                        } else if (size > coefMb) {
                            sufix = " Mb";
                            humanSize = Math.round(size * 100 / coefMb) / 100;
                        } else {
                            sufix = " Kb";
                            humanSize = Math.round(size * 100 / coefKb) / 100;
                        }

                        return humanSize.toString() + sufix;
                    },
                    fn: function () { },
                    entity:{},
                    checkAndAssign: {
                        fn: function (s,d,p) {
                            if (s) {
                                if (typeof (s) === typeof (local.fn)) {
                                    d[p] = s;
                                }
                            }
                        },
                        obj: function (s,d, p) {
                            if (s) {
                                d[p] = s;
                            }
                        },
                        entity: function (s,d, entityTypeOf) {
                            if (s && entityTypeOf) {
                                if (typeof (s) === entityTypeOf) {
                                    d = s;
                                }
                            }
                        },
                    },
                    checkPasswordSecurity: function (pw) {
                        //'Facilite una contraseña de mínimo 6 caracteres. Use números, letras (Mayúsculas y minúsculas), y/o los siguientes símbolos autorizados: <strong style="text-decoration: underline">$ % @ . , - _ ! ¡ ¿ ? # / \ · ( )</u></strong>. Por seguridad, no comparta esta contraseña con nadie, o hágalo sólo si es imprescindible.',
                        var allowedPasswordChars = 'ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890$%@.,-_!¡¿?#/\·()',
                            x = 0;

                        if (pw.length < 6) {
                            return false;
                        } else {

                            for (x = 0; x < pw.length ; x++) {
                                try {
                                    if (allowedPasswordChars.indexOf(pw[x], 0) < 0) {
                                        return false;
                                    }
                                } catch (e) {
                                    return false;
                                }
                            }                            
                        }

                        return true;
                    },
                    validateEmail: function (email) {                        
                        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                        return re.test(email);                    
                    },
                };

            self.pad = local.pad;
            self.getByteSize = local.getByteSize;
            self.arrayBufferToBase64 = local.arrayBufferToBase64;
            self.checkAndAssign = local.checkAndAssign;
            self.validateEmail = local.validateEmail;
            self.checkPasswordSecurity = local.checkPasswordSecurity;
            return self;
        }
    ]);

}(window));
