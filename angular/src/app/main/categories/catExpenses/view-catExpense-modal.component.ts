import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetCatExpenseForViewDto, CatExpenseDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewCatExpenseModal',
    templateUrl: './view-catExpense-modal.component.html'
})
export class ViewCatExpenseModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetCatExpenseForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetCatExpenseForViewDto();
        this.item.catExpense = new CatExpenseDto();
    }

    show(item: GetCatExpenseForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
