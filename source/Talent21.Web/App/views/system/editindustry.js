app.controller('industryeditController', ['$scope', 'dataService', function ($scope, db) {
    
    $scope.save = function (record) {
        record.system = [];
        db.system.addIndustry(record).success(function (refreshRecord) {
            console.log(refreshRecord);
        });
    }

    function refreshRecord() {
        return db.system.getIndustries().success(function (result) {
            $scope.industries = result;
        });
    }   

    $scope.update = function (i) {
        db.system.editIndustry(i).success(refreshRecord);
    };


    $scope.delete = function (i) {
        db.system.deleteIndustry(i).success(function (result) {
        });
    }

    $scope.toggle = function (i) {
        i.editMode = !i.editMode;
    };
    refreshRecord();
}]);