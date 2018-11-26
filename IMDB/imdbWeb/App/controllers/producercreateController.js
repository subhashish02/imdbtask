'use strict';
app.controller('producercreateController', ['$scope', '$rootScope', '$filter',  'localStorageService', '$location', '$modalInstance', function ($scope, $rootScope, $filter,  localStorageService, $location, $modalInstance) {
    debugger;
    try {
        $scope.message = "";
        $scope.onlyNumbers = /^\d+$/;
        $scope.mode = $rootScope.mode;
        if ($scope.mode == "NEW") {
            $scope.modaltitle = "Create New Mobile producer"
            $scope.producerData = {};
            $scope.producerData.uname = "";
            $scope.producerData.aname = "";
            $scope.producerData.mobile = "";
            $scope.producerData.email = "";
        }
        else {
            $scope.modaltitle = "Edit Mobile producer Details";
            $scope.producerData = $rootScope.udata;
        }
        $rootScope.udata = null;
        $rootScope.mode = null;
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.cancel();

        $scope.createproducer = function () {
            debugger;
            $scope.message = "";
            if ($scope.mode == 'NEW') {
                $scope.busymsg = "Creating New producer. Please Wait..."
                $scope.saveBusy = true;
                $scope.myPromise = genericService.genericFunction('POST', 'producer', $scope.producerData, 0, "");
                $scope.myPromise.then(function (results) {

                    debugger;
                    $scope.message = results.message;
                    $scope.saveBusy = false;
                    if(results.status!='error')
                        $modalInstance.close(results);
                });
            }
            else {
                $scope.busymsg = "Updating producer Details. Please Wait..."
                $scope.saveBusy = true;
                $scope.myPromise = genericService.genericFunction('PUT', 'producer', $scope.producerData, scope.producerData.uid, "");
                $scope.myPromise.then(function (results) {

                    debugger;
                    $scope.message = results.message;
                    $scope.saveBusy = false;
                    if (results.status != 'error')
                        $modalInstance.close(results);
                });
            }
        }

		$scope.deleteproducer = function (snid) {
            $scope.busymsg = "deleting session Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('DELETE', 'producer', {}, scope.producerData.uid, "");
            $scope.myPromise.then(function (result) {
                
                if (result.bstatus == true) {
                    $scope.msuccess = true;
                    $scope.isOpen = false;
                    $scope.msg = "producer deleted successfully.";
                    $scope.getSessions();
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