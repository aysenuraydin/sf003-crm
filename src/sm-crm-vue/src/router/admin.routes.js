import CustomerList from '@/views/Customers/CustomerList.vue'
import TitleList from '@/views/Titles/TitleList.vue'
import DepartmentList from '@/views/Departments/DepartmentList.vue'
import EmployeeList from '@/views/Employees/EmployeeList.vue'
import HomeView from '@/views/HomeView.vue'
import SaleList from '@/views/Sales/SaleList.vue'
import TaskList from '@/views/Tasks/TaskList.vue'
import UserAddressList from '@/views/UserAddresses/UserAddressList.vue'
import UserEmailList from '@/views/UserEmails/UserEmailList.vue'
import AdminLayout from '@/views/_Layouts/AdminLayout.vue'
import RequestStatusList from '@/views/RequestStatuses/RequestStatusList.vue'
import DocumentTypeList from '@/views/DocumentTypes/DocumentTypeList.vue'
import TerritoryList from '@/views/Territories/TerritoryList.vue'
import RequestList from '@/views/Requests/RequestList.vue'
import NotificationList from '@/views/notification/NotificationList.vue'

const adminLayoutPages = {
    path: '/',
    component: AdminLayout,
    name: 'admin',
    meta: { authorize: true },
    children: [
        { path: '', component: HomeView, name: 'home' },
        { path: 'employees', component: EmployeeList, name: 'employees' },
        { path: 'customers', component: CustomerList, name: 'customers' },
        { path: 'useremails', component: UserEmailList, name: 'useremails' },
        { path: 'useraddresses', component: UserAddressList, name: 'userAddresses' },
        { path: 'sales', component: SaleList, name: 'sales' },
        { path: 'tasks', component: TaskList, name: 'tasks' },
        { path: 'requests', component: RequestList, name: 'requests' },
        { path: 'notification', component: NotificationList, name: 'notification' },
        // LST
        { path: 'departments', component: DepartmentList, name: 'departments' },
        { path: 'titles', component: TitleList, name: 'titles' },
        { path: 'requeststatuses', component: RequestStatusList, name: 'requeststatuses' },
        { path: 'documenttypes', component: DocumentTypeList, name: 'documenttypes' },
        { path: 'territories', component: TerritoryList, name: 'territories' },

        {
            meta: { authorize: true },
            path: '/customers2', name: 'customers2',
            // route level code-splitting
            // this generates a separate chunk (About.[hash].js) for this route
            // which is lazy-loaded when the route is visited.
            component: () => import('@/views/Customers/CustomerList.vue')
        },
    ]
}

export default adminLayoutPages