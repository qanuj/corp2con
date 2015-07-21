app.controller('industryeditController', ['$scope', 'dataService', function ($scope, db) {
    
    
    function refreshRecord(page) {
        return db.system.getIndustries(page).success(function (result) {
            $scope.system = result;
        });
    }
    $scope.save = function (record) {
        record.system = [];
        db.system.addIndustry(record).success(function (result) {
            console.log(result);
        });
    }

    $scope.update = function (i) {
        db.system.EditIndustry(i).success(function (result) {
        });
    }

    $scope.delete = function (i) {
        db.system.DeleteIndustry(i).success(function (result) {
        });
    }

    $scope.toggle = function (i) {
        i.editMode = !i.editMode;
    };
    refreshRecord();
}]);