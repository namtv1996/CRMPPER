import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CatExpensesComponent } from './categories/catExpenses/catExpenses.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'categories/catExpenses', component: CatExpensesComponent, data: { permission: 'Pages.CatExpenses' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
