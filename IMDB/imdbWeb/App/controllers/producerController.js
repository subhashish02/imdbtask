'use strict';
app.controller('producerController', ['$scope', '$rootScope', '$filter',  'localStorageService', '$location', '$uibModal', '$routeParams',  function ($scope, $rootScope, $filter,  localStorageService, $location, $uibModal, $routeParams) {
    debugger;
    $rootScope.mode = '';
    $scope.reverseSort = false;
    $scope.filterData = "";
    $scope.producerList = [];
    $scope.tableData = [];
    try {
        $scope.Getproducers = function () {
            $scope.busymsg = "Getting producer Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('GET', 'producer', {}, 0, "");
            $scope.myPromise.then(function (results) {
                debugger;
                $scope.producerList = results;
                $scope.saveBusy = false;
            });
        }
        $scope.Getproducers();

        $scope.createClick = function (mode, udata) {
            debugger;
            $rootScope.mode = mode;
            $rootScope.udata = udata;
            var modalInstance = $modal.open({
                templateUrl: "producercreate.html",
                controller: 'producercreateController',
            });
            modalInstance.result.then(function (result) {
                $scope.Getproducers();

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
                templateUrl: "producercreate.html",
                controller: 'producercreateController',
            });
            modalInstance.result.then(function (result) {
                $scope.Getproducers();

            }, function () {
                debugger;
                console.info('Modal dismissed at: ' + new Date());
            });
        };

    }
    catch (e) {
    }



}]);