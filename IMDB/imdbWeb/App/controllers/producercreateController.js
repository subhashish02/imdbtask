'use strict';
app.controller('producercreateController', ['$scope', '$rootScope', '$filter', 'localStorageService', '$location', '$modalInstance', 'genericService', function ($scope, $rootScope, $filter, localStorageService, $location, $modalInstance, genericService) {
    debugger;
    try {
        $scope.message = "";
        $scope.producer = {};


        $scope.cancel = function (obj) {
            $modalInstance.dismiss(obj);
        };

        $scope.createproducer = function () {
            debugger;
            $scope.message = "";
            $scope.busymsg = "Creating New producer. Please Wait..."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('POST', 'Producer', $scope.producer, 0);
            $scope.myPromise.then(function (results) {

                debugger;
                $scope.message = results.message;
                $scope.saveBusy = false;
                if (results.status != 'error') {
                    $scope.producer.proid = results.lid;
                    $modalInstance.close($scope.producer);
                }                 
            });
        }


        $scope.deleteproducer = function (proid) {
            $scope.busymsg = "deleting producer Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('DELETE', 'Producer', {}, proid);
            $scope.myPromise.then(function (result) {

                if (result.bstatus == true) {
                    $scope.msuccess = true;
                    $scope.isOpen = false;
                    $scope.msg = "producer deleted successfully.";
                }
                else {
                    $scope.msuccess = false;
                    $scope.msg = result.bmsg;
                }

                $scope.saveBusy = false;
            });
        }
    }
    catch (e) { }
}]);