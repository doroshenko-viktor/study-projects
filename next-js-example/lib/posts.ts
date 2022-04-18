import path from "path";
import matter from "gray-matter";
import fs from "fs";
import { remark } from "remark";
import html from "remark-html";
import { HeadPostData } from "../types/post";

const CURRENT_DIR = process.cwd();
const POSTS_DIR = path.join(CURRENT_DIR, "posts");

export function getSortedPostsData() {
  const fileNames = fs.readdirSync(POSTS_DIR);
  const allPostsData: HeadPostData[] = fileNames.map((fileName) => {
    const id = getFileId(fileName);
    const fullPath = path.join(POSTS_DIR, fileName);
    const fileContents = fs.readFileSync(fullPath, "utf8");
    const matterResult = matter(fileContents);

    return {
      id,
      title: matterResult.data.title,
      date: matterResult.data.date,
    };
  });

  return allPostsData.sort(({ date: a }, { date: b }) => {
    if (a < b) {
      return 1;
    } else if (a > b) {
      return -1;
    } else {
      return 0;
    }
  });
}

export function getAllPostIds() {
  return fs.readdirSync(POSTS_DIR).map((name) => {
    return {
      params: {
        id: getFileId(name),
      },
    };
  });
}

export async function getPostData(id) {
  const fullPath = path.join(POSTS_DIR, `${id}.md`);
  const fileContents = fs.readFileSync(fullPath, "utf8");
  const matterResult = matter(fileContents);

  const processedContent = await remark()
    .use(html)
    .process(matterResult.content);
  const blogHtmlContent = processedContent.toString();

  return { id, ...matterResult.data, content: blogHtmlContent };
}

function getFileId(name) {
  return name.replace(/\.md$/, "");
}
