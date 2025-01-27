import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'accounts',
        pathMatch: 'full'
    },
    {
        path: 'accounts',
        children: [
            {
                path: '',
                loadComponent: () => import('./views/account/accounts-list/accounts-list.component').then(m => m.AccountsListComponent)
            },
            {
                path: ':accountId',
                loadComponent: () => import('./views/account/account-details/account-details.component').then(m => m.AccountDetailsComponent)
            }
        ]
    }
];
