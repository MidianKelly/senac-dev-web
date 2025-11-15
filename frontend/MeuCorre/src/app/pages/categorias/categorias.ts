import { Component, computed, inject, OnInit, signal, TemplateRef, WritableSignal } from '@angular/core';
import { ModalDismissReasons, NgbModal, NgbNavModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoriaModel } from './models/categoria.model';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { IconAvatar } from '../../shared/components/icon-avatar/icon-avatar';
import { StatusBadge } from '../../shared/components/status-badge/status-badge';
import { CategoriaService } from './categoria.service';


@Component({
  selector: 'app-categorias',
  imports: [NgbNavModule, IconAvatar, StatusBadge, ReactiveFormsModule, NgbTooltipModule],
  templateUrl: './categorias.html',
  styleUrl: './categorias.css',
})
export class Categorias implements OnInit {
  
  private modalService = inject(NgbModal);

  private categoriaService = inject(CategoriaService);

  closeResult: WritableSignal<string> = signal('');

  nome = new FormControl('');
  descricao = new FormControl('');
  cor = new FormControl('');
  icone = new FormControl('');
  tipo = new FormControl('despesa');


  active = 1;
  editandoCategoria = false;
  idEditandoCategoria = '';

  categorias = signal<CategoriaModel[]>([]);

  
  listaReceitas = computed(() => this.categorias().filter((c) => c.tipo ==="receita"));
  listaDespesas = computed(() => this.categorias().filter((c) => c.tipo ==="despesa"));


  ngOnInit(): void {
    this.carregarTodasCategorias();
  }

  carregarTodasCategorias(){
    this.categoriaService.obterTodasPorUsuario().subscribe({
      next:(dados) => {

        const categoriasMapeadas = dados.map((c) => {
          const item: CategoriaModel = {
            id: c.id,
            nome: c.nome,
            descricao: c.descricao,
            cor: c.cor,
            icone: c.icone,
            tipo: c.tipo === '1' ? 'receita' : 'despesa',
            ativo: c.ativo

          }
          return item; 
        });
        this.categorias.set(categoriasMapeadas);
      },
      error:(err) => {
        console.error('Erro ao carregar categorias', err);
      }
    })
  }

  open(content: TemplateRef<any>, categoria?: CategoriaModel) {
    if ((categoria)) {

      this.idEditandoCategoria = categoria.id;
      this.editandoCategoria = true;
      this.nome.setValue(categoria.nome);
      this.descricao.setValue(categoria.descricao);
      this.cor.setValue(categoria.cor);
      this.icone.setValue(categoria.icone);
      this.tipo.setValue(categoria.tipo);

    } else {

      this.nome.setValue('');
      this.descricao.setValue('');
      this.cor.setValue('');
      this.icone.setValue('');
    }
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
      (result) => {},

      (reason) => {
        this.closeResult.set(`Dismissed ${this.getDismissReason(reason)}`);
      },
    );
  }

  private getDismissReason(reason: any): string {
    switch (reason) {
      case ModalDismissReasons.ESC:
        return 'by pressing ESC';
      case ModalDismissReasons.BACKDROP_CLICK:
        return 'by clicking on a backdrop';
      default:
        return `with: ${reason}`;
    }
  }

  cadastrarCategoria() {
    console.log(this.nome.value);
    console.log(this.descricao.value);
    console.log(this.cor.value);
    console.log(this.icone.value);

    const novaCategoria: CategoriaModel = {
      id: '',
      nome: this.nome.value!,
      descricao: this.descricao.value!,
      cor: this.cor.value!,
      icone: this.icone.value!,
      tipo: '',
      ativo: true
    };

    if (this.active === 1) {
      novaCategoria.tipo = 'despesa';
      this.categorias().push(novaCategoria);
    } else {
      novaCategoria.tipo = 'receita';
      this.categorias().push(novaCategoria);
    }

    this.modalService.dismissAll();
  }


  deletarCategoria(id: string) {
  }

  editarCategoria() {
    const categoria = this.categorias().find(cat => cat.id === this.idEditandoCategoria);
    if (categoria) {
      categoria.nome = this.nome.value!;
      categoria.descricao = this.descricao.value!;
      categoria.cor = this.cor.value!;
      categoria.icone = this.icone.value!;
    }
    console.log(this.categorias)
    this.modalService.dismissAll();
  }



}