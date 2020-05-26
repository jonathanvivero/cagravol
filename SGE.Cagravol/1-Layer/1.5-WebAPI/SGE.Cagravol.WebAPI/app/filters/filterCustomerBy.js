(function (angular) {
    'use strict';

    var sge = angular.module('sgeApp');

    sge.filter('filterCustomerBy', function () {
        return function (items, sortField, listFilter) {
            var filtered = [];

            angular.forEach(items, function (item) {
                if (item.spaceNumber > 0)
                    if (listFilter) {
                        if (listFilter.id === 'free' && item.email === null) {
                            filtered.push(item);
                        } else if (listFilter.id === 'ocupied' && item.email !== null) {
                            filtered.push(item);
                        } else if (listFilter.id === 'all') {
                            filtered.push(item);
                        }
                    } else {
                        filtered.push(item);
                    }


            });

            //filtered.sort(function (a, b) {
            //    return (a[sortField] > b[sortField] ? 1 : -1);
            //});

            //if (reverse) {
            //    filtered.reverse();
            //}

            return filtered;
        };
    });
}(window.angular));

