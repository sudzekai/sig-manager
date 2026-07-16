import Select, { type Props } from "react-select";

export function CustomSelect<Option, IsMulti extends boolean = false>(
    props: Props<Option, IsMulti>
) {
    return (
        <Select
            classNamePrefix="rs"
            isSearchable
            {...props}
            classNames={{
                control: (state) =>
                    `rounded-xl flex flex-row min-h-[40px] px-2 p-1
                     bg-(--background-primary)/30
                     hover:bg-(--background-primary)/40
                     ring-inset ring-1 
                     ${state.isFocused ? "ring-2 ring-(--accent-primary)" : "ring-(--border-primary) hover:ring-(--border-primary)/80"}`,
                valueContainer: () => "bg-(--background-primary)/0 flex flex-wrap gap-1",
                menuList: () => `bg-(--background-primary)/90 rounded-xl`,
                option: (state) => `
                w-full flex flex-row px-3 py-2 hover:bg-(--accent-primary)/60 cursor-pointer
                ${state.isSelected ? "bg-(--accent-primary)/90 font-semibold" : ""}`,
                multiValue: () => `flex flex-row gap-1 items-center justify-center rounded-xl bg-(--accent-primary) me-1`,
                multiValueLabel: () => `text-white ps-2 pe-1 py-1`,
                multiValueRemove: () => `btn btn-danger px-2 p-1 me-1`,
            }}
            styles={{
                control: () => ({}),
                menu: (base) => ({
                    ...base,
                    background: "transparent"
                }),
                menuList: (base) => ({
                    ...base,
                    maxHeight: '250px'
                }),
                input: (base) => ({
                    ...base,
                    color: "var(--text-primary)",
                    "::selection": { background: "rgb(from var(--accent-primary) r g b / 0.5)" }
                }),
                placeholder: (base) => ({
                    ...base,
                    color: "var(--text-muted)",
                }),
                valueContainer: (base) => ({ ...base, padding: "0" }),
                singleValue: (base) => ({ ...base, color: "var(--text-primary)" }),
                option: () => ({}),
                multiValue: () => ({}),
                multiValueLabel: () => ({}),
                multiValueRemove: () => ({}),
            }}
        >

        </Select>
    );
}