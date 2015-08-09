app.controller('industryController', ['$scope', 'dataService', function ($scope, db) {

    $scope.navigate = function (page) {
        refreshRecord();
        db.system.pagedIndustries(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
            console.log(result);
        });

        $scope.save = function (record) {
            $('input[type=text]').each(function () {
                $(this).val('');
            });
            db.system.addIndustry(record).success(refreshRecord);
        }

        $scope.update = function (record) {
            db.system.updateIndustry(record).success(refreshRecord);
        }

        $scope.delete = function (i) {
            db.system.deleteIndustry(i).success(refreshRecord);
        }

        $scope.toggle = function (i) {
            i.editMode = !i.editMode;
        };
        function refreshRecord() {
            return db.system.getIndustries().success(function (result) {
                $scope.records = result.items;
            });
        }
    }

    $scope.navigate(params.page);
}]);