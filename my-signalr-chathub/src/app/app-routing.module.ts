// app-routing.module.ts

import { RouterModule, Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'supertienda',
    loadComponent: () => import('../st-table/st-table.component').then(m => m.SupertiendaComponent)
  },
  // ... other routes
];

export const AppRoutingModule = RouterModule.forRoot(routes);
