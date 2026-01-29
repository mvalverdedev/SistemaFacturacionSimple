import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { UserService, Usuario } from '../../services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
  standalone: false
})
export class UserListComponent implements OnInit {
  displayedColumns: string[] = ['nombreUsuario', 'nombreCompleto', 'rol', 'acciones'];
  dataSource = new MatTableDataSource<Usuario>();
  totalRecords = 0;
  pageSize = 10;
  pageIndex = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.cargarUsuarios();
  }

  cargarUsuarios() {
    this.userService.obtenerUsuarios(this.pageIndex + 1, this.pageSize)
      .subscribe({
        next: (response) => {
          this.dataSource.data = response.datos;
          this.totalRecords = response.totalRecords;
        },
        error: (err) => {
          this.toastr.error('Error al cargar usuarios', 'Error');
        }
      });
  }

  editarUsuario(usuario: Usuario) {
    this.userService.establecerUsuarioSeleccionado(usuario);
    this.router.navigate(['/usuarios/editar', usuario.id]);
  }

  cambiarPagina(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.cargarUsuarios();
  }
}
