'use strict';
app.controller('actorcreateController', ['$scope', '$rootScope', '$filter',  'localStorageService', '$location', '$modalInstance', function ($scope, $rootScope, $filter,  localStorageService, $location, $modalInstance) {
    debugger;
    try {
        $scope.message = "";
        $scope.onlyNumbers = /^\d+$/;
        $scope.mode = $rootScope.mode;
        if ($scope.mode == "NEW") {
            $scope.modaltitle = "Create New Mobile actor"
            $scope.actorData = {};
            $scope.actorData.uname = "";
            $scope.actorData.aname = "";
            $scope.actorData.mobile = "";
            $scope.actorData.email = "";
        }
        else {
            $scope.modaltitle = "Edit Mobile actor Details";
            $scope.actorData = $rootScope.udata;
        }
        $rootScope.udata = null;
        $rootScope.mode = null;
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.cancel();

        $scope.createactor = function () {
            debugger;
            $scope.message = "";
            if ($scope.mode == 'NEW') {
                $scope.busymsg = "Creating New actor. Please Wait..."
                $scope.saveBusy = true;
                $scope.myPromise = genericService.genericFunction('POST', 'actor', $scope.actorData, 0, "");
                $scope.myPromise.then(function (results) {

                    debugger;
                    $scope.message = results.message;
                    $scope.saveBusy = false;
                    if(results.status!='error')
                        $modalInstance.close(results);
                });
            }
            else {
                $scope.busymsg = "Updating actor Details. Please Wait..."
                $scope.saveBusy = true;
                $scope.myPromise = genericService.genericFunction('PUT', 'actor', $scope.actorData, scope.actorData.uid, "");
                $scope.myPromise.then(function (results) {

                    debugger;
                    $scope.message = results.message;
                    $scope.saveBusy = false;
                    if (results.status != 'error')
                        $modalInstance.close(results);
                });
            }
        }

		$scope.deleteactor = function (snid) {
            $scope.busymsg = "deleting session Please Wait.."
            $scope.saveBusy = true;
            $scope.myPromise = genericService.genericFunction('DELETE', 'actor', {}, scope.actorData.uid, "");
            $scope.myPromise.then(function (result) {
                
                if (result.bstatus == true) {
                    $scope.msuccess = true;
                    $scope.isOpen = false;
                    $scope.msg = "actor deleted successfully.";
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