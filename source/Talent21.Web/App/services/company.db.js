app.factory('companyService', [
    '$http', '$q', 'db.util', function ($http, $q, util) {
        var v = 'api/v1/';

        var calculatePaging = util.calculatePaging;
        var orderBy = util.orderBy;

        var company = {};
        
        company.paged = function (page, pageSize) {
            return $http.get(v + 'company/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        company.bench = function (query, page, pageSize, order) {
            return $http.post(v + 'company/bench?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id'), query);
        }

        company.dashboard = function () {
            return $http.get(v + 'company/dashboard');
        }

        company.sendInvites  = function (rows) {
            return $http.put(v + 'company/bench/invite', rows);
        }

        company.balance = function () {
            return $http.get(v + 'company/balance');
        }
        
        company.all = function () {
            return $http.get(v + 'company/all');
        }

        company.saveApplication = function (id, folder) {
            return $http.put(v + 'company/job/application/' + id + '/move/' + folder);
        }

        company.moveContractor = function (id, folder) {
            return $http.put(v + 'company/contractor/' + id + '/move/' + folder);
        }

        company.shortlistApplication = function (id) {
            return $http.put(v + 'company/job/application/' + id + '/shortlist');
        }

        company.rejectApplication = function (id) {
            return $http.put(v + 'company/job/application/' + id + '/reject');
        }

        company.get = function (id) {
            return $http.get(v + 'company/profile' + (!!id ? '/' + id : ''));
        }

        company.update = function (formData) {
            return $http.put(v + 'company/profile', formData);
        }
        
        company.myJobs = function (page, pageSize) {
            return $http.get(v + 'company/job/paged?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
        }
        
        company.transactions = function (page, pageSize) {
            return $http.get(v + 'company/transaction?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
        }

        company.addCredits = function (credits) {
            return $http.post(v + 'company/credits/'+credits);
        }

        company.limitJobs = function (page, pageSize) {
            return $http.get(v + 'company/job/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        company.search = function (query, page, pageSize, order) {
            return $http.post(v + 'company/search?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id'), query);
        }

        company.getLatestProfiles = function (skill, location, page, pageSize) {
            return $http.get(v + 'company/latest/profiles/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page, pageSize));
        }

        company.getTopProfiles = function (skill, location, page, pageSize) {
            return $http.get(v + 'company/top/profiles/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page, pageSize));
        }

        company.getJobApplications = function (id, page, pageSize) {
            return $http.get(v + 'company/job/' + id + '/applications/paged?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id desc'));
        }

        company.getJobApplication = function (id) {
            return $http.get(v + 'company/job/application/' + id);
        }

        company.getSchedule = function (id) {
            return $http.get(v + 'company/schedule/' + id);
        }

        company.visitContractor = function (id) {
            return $http.get(v + 'company/contractor/'+id+'/visit');
        }

        company.getFolders = function (jobId) {
            return $http.get(v + 'company/job/' + jobId + '/folders');
        }
        
        company.getSearchFolders = function () {
            return $http.get(v + 'company/contractor/folders');
        }

        company.getBenchFolders = function () {
            return $http.get(v + 'company/bench/folders');
        }

        return company;

    }]);