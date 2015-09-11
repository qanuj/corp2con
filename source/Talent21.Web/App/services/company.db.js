app.factory('companyService', [
    '$http', '$q', 'db.util', function ($http, $q, util) {
        var v = 'api/v1/company/';

        var calculatePaging = util.calculatePaging;
        var orderBy = util.orderBy;

        var company = {};

        company.paged = function (page, pageSize) {
            return $http.get(v + 'paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        company.bench = function (query, page, pageSize, order) {
            return $http.post(v + 'bench?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id'), query);
        }

        company.dashboard = function () {
            return $http.get(v + 'dashboard');
        }

        company.sendInvites  = function (rows) {
            return $http.put(v + 'bench/invite', rows);
        }

        company.all = function () {
            return $http.get(v + 'all');
        }

        company.saveApplication = function (id, folder) {
            return $http.put(v + 'job/application/' + id + '/move/' + folder);
        }

        company.moveContractor = function (id, folder) {
            return $http.put(v + 'contractor/' + id + '/move/' + folder);
        }

        company.inviteToJob = function (id, job) {
            return $http.put(v + 'contractor/invite',{jobId:job,contractorId:id});
        }

        company.shortlistApplication = function (id) {
            return $http.put(v + 'job/application/' + id + '/shortlist');
        }

        company.rejectApplication = function (id) {
            return $http.put(v + 'job/application/' + id + '/reject');
        }

        company.get = function (id) {
            return $http.get(v + 'profile' + (!!id ? '/' + id : ''));
        }

        company.update = function (formData) {
            return $http.put(v + 'profile', formData);
        }
        
        company.myJobs = function (page, pageSize) {
            return $http.get(v + 'job/paged?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
        }
        

        company.balance = function () {
            return $http.get(v + 'balance');
        }

        company.transactions = function (page, pageSize) {
            return $http.get(v + 'transaction?$inlinecount=allpages&$orderby=Id desc' + calculatePaging(page, pageSize));
        }

        company.addCredits = function (credits) {
            return $http.post(v + 'credits/'+credits);
        }

        company.limitJobs = function (page, pageSize) {
            return $http.get(v + 'job/paged?$inlinecount=allpages' + calculatePaging(page, pageSize));
        }

        company.search = function (query, page, pageSize, order) {
            return $http.post(v + 'search?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id'), query);
        }

        company.getLatestProfiles = function (skill, location, page, pageSize) {
            return $http.get(v + 'latest/profiles/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page, pageSize));
        }

        company.getTopProfiles = function (skill, location, page, pageSize) {
            return $http.get(v + 'top/profiles/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page, pageSize));
        }

        company.getJobApplications = function (id, page, pageSize) {
            return $http.get(v + 'job/' + id + '/applications/paged?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id desc'));
        }

        company.getJobApplication = function (id) {
            return $http.get(v + 'job/application/' + id);
        }

        company.getSchedule = function (id) {
            return $http.get(v + 'schedule/' + id);
        }

        company.visitContractor = function (id) {
            return $http.get(v + 'contractor/'+id+'/visit');
        }

        company.getFolders = function (jobId) {
            return $http.get(v + 'job/' + jobId + '/folders');
        }
        
        company.getSearchFolders = function () {
            return $http.get(v + 'contractor/folders');
        }

        company.getActiveJobs = function () {
            return $http.get(v + 'job/active?$orderby=Label');
        }

        company.getBenchFolders = function () {
            return $http.get(v + 'bench/folders');
        }

        company.promoteJob = function (jobId, promo) {
            return $http.post(v + 'job/promote/' + jobId + '/' + promo);
        }

        company.promote = function(p) {
            return $http.post(v+'promote/'+p)
        }

        return company;

    }]);