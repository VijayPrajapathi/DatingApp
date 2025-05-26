export interface Message {
  id: number
  senderId: number
  senderUserName: string
  senderPhotoUrl: string
  recipientId: number
  recipientPhotoUrl: string
  recipientUserName: string
  content: string
  dateRead?: Date
  messageSentDate: Date
}