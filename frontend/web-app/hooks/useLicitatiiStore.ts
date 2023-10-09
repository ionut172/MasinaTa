import { Licitatii, PagedResults } from "@/types"
import { create } from "zustand"

type State = {
    Licitatii: Licitatii[]
    totalCount: number
    pageCount: number
}

type Actions = {
    setData: (data: PagedResults<Licitatii>) => void
    setCurrentPrice: (LicitatiiId: string, pretRezervare: number) => void
}

const initialState: State = {
    Licitatii: [],
    pageCount: 0,
    totalCount: 0
}

export const useLicitatiiStore = create<State & Actions>((set) => ({
    ...initialState,

    setData: (data: PagedResults<Licitatii>) => {
        set(() => ({
            Licitatiis: data.result,
            totalCount: data.totalCount,
            pageCount: data.pageCount
        }))
    },

    setCurrentPrice: (LicitatiiId: string, pretRezervare: number) => {
        set((state) => ({
            Licitatii: state.Licitatii.map((Licitatii) => Licitatii.id === LicitatiiId 
                ? {...Licitatii, currentHighBid: pretRezervare} : Licitatii)
        }))
    }
}))