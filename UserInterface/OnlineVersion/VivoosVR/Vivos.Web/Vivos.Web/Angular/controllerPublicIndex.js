(function () {
    'use strict';

    angular
        .module('app')
        .controller('controllerPublicIndex', controllerPublicIndex);

    controllerPublicIndex.$inject = ['$location', '$scope', '$http', '$compile', 'ngNotify', '$rootScope'];

    function controllerPublicIndex($location, $scope, $http, $compile, ngNotify, $rootScope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'controllerPublicIndex';

        activate();

        $scope.Assets = [];

        activate();

        function activate() { }

        $(document).ready(function (e) {
            

            //$http.get("../api/envanter/harekettipleri").then(function (e) {
            //    $scope.hareketTipleri = e.data.Items;
            //});

            //$('.date-picker').datetimepicker(
            //{
            //    format: 'DD.MM.YYYY',
            //    inline: false,
            //    sideBySide: true
            //});
        });

        

    }
})();
