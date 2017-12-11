(function () {
    'use strict';

    angular
        .module('app')
        .controller('controllerCustomerIndex', controllerCustomerIndex);

    controllerCustomerIndex.$inject = ['$location', '$scope', '$http', '$compile', 'ngNotify', '$rootScope'];

    function controllerCustomerIndex($location, $scope, $http, $compile, ngNotify, $rootScope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'controllerCustomerIndex';

        activate();

        function activate() { }

        $(document).ready(function (e) {
            LoadItems();
        });

        function LoadItems() {
            $http.get("../api/customer/getmyassetswithimages").then(function (e) {
                $scope.AssetsIOwned = e.data.Assets;
            }, function(e) {
                ngNotify.set(e.data.ExceptionMessage, 'error');
            });

            $http.get("../api/customer/getnotmyassetswithimages").then(function (e) {
                $scope.AssetsIMayOwn = e.data.Assets;
            }, function (e) {
                ngNotify.set(e.data.ExceptionMessage, 'error');
            });
        }

    }
})();
