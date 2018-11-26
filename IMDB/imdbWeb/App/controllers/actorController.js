'use strict';
app.controller('actorController', ['$scope', '$rootScope', '$filter',  'localStorageService', '$location', '$uibModal', '$routeParams',  function ($scope, $rootScope, $filter,  localStorageService, $location, $uibModal, $routeParams) {
    debugger;
    $rootScope.mode = '';
    $scope.reverseSort = false;
    $scope.filterData = "";
    $scope.actorList = [];
    $scope.tableData = [];
    try {
        $scope.Getactors = function () {
            $scope.busymsg = "Getting actor Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('GET', 'actor', {}, 0, "");
            $scope.myPromise.then(function (results) {
                debugger;
                $scope.actorList = results;
                $scope.saveBusy = false;
            });
        }
        $scope.Getactors();

        $scope.createClick = function (mode, udata) {
            debugger;
            $rootScope.mode = mode;
            $rootScope.udata = udata;
            var modalInstance = $modal.open({
                templateUrl: "actorcreate.html",
                controller: 'actorcreateController',
            });
            modalInstance.result.then(function (result) {
                $scope.Getactors();

            }, function () {
                debugger;
                console.info('Modal dismissed at: ' + new Date());
            });
        };

       $scope.createClick = function (mode, udata) {
            debugger;
            $rootScope.mode = mode;
            $rootScope.udata = udata;
            var modalInstance = $modal.open({
                templateUrl: "actorcreate.html",
                controller: 'actorcreateController',
            });
            modalInstance.result.then(function (result) {
                $scope.Getactors();

            }, function () {
                debugger;
                console.info('Modal dismissed at: ' + new Date());
            });
        };

    }
    catch (e) {
    }



}]);