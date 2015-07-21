app.controller('functionalController', ['$scope', 'dataService', function ($scope, db) {

    $scope.save = function (record) {
        record.functionalArea = [];
        db.system.addFunctionalArea(record).success(function (refreshRecord) {
            console.log(refreshRecord);
        });
    }

    function refreshRecord() {
        return db.system.getFunctionalArea().success(function (result) {
            $scope.functionalArea = result;
        });
    }
    $scope.update = function (s) {
        db.system.editFunctionalArea(s).success(refreshRecord);
    };

    $scope.delete = function (s) {
        db.system.deleteFunctionalArea(s).success(result);
    };

    $scope.toggle = function (s) {
        s.editMode = !s.editMode;
    };
    refreshRecord();
}]);

