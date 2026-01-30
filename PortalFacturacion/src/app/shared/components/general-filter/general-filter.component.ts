import { Component, EventEmitter, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSidenavModule, MatDrawer } from '@angular/material/sidenav';

/**
 * Componente Search
 * 
 * Componente reutilizable para búsqueda, con integración de drawer lateral para configuración u opciones adicionales.
 * 
 * @example
 * <app-general-filter (search)="onBuscar()"></app-general-filter>
 */
@Component({
    selector: 'app-general-filter',
    standalone: true,
    imports: [
        CommonModule,
        MatSidenavModule,
        MatButtonModule,
        MatIconModule,
        MatTooltipModule
    ],
    templateUrl: './general-filter.component.html',
    styleUrls: ['./general-filter.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class GeneralFilterComponent {
    /**
     * Evento emitido cuando se realiza la acción de búsqueda.
     */
    @Output() search = new EventEmitter<void>();

    /**
     * Referencia al drawer de configuración.
     */
    @ViewChild('settingsDrawer') settingsDrawer?: MatDrawer;

    /**
     * Ejecuta la búsqueda y cierra el drawer si está abierto.
     */
    onSearch(): void {
        this.search.emit();
        if (this.settingsDrawer) {
            this.settingsDrawer.close();
        }
    }
}
