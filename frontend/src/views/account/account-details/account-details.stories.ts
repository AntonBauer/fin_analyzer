import { Meta, StoryObj } from "@storybook/angular";
import { AccountDetailsComponent } from "./account-details.component";

const meta: Meta<AccountDetailsComponent> = {
    title: 'Account Details',
    component: AccountDetailsComponent,
    tags: ['autodocs']
};

export default meta;

type Story = StoryObj<AccountDetailsComponent>;

export const Default: Story = {
    args: {

    }
};