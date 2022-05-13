export interface HeadPostData {
  date: string;
  title: string;
  id: string;
}

export interface PostData extends HeadPostData {
  content: string;
}
